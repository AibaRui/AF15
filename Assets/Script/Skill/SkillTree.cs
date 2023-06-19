using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill;
using System.IO;

public class SkillTree : MonoBehaviour
{

    [SerializeField, Tooltip("スキルツリーのButtonの配列")]
    private Button[] _buttons;

    [SerializeField, Tooltip("連打防止のパネルオブジェクトの配列")]
    private GameObject[] _panels;

    [SerializeField, Tooltip("隣接リストに格納するようの数値が書かれたCSV")]
    private TextAsset _skillTreeSheet;

    [Tooltip("ISkillableを継承しているクラスを設定できる")]
    [SerializeField, SerializeReference, SubclassSelector]
    private ISkillable[] _skills;

    private List<int>[] _skillList;//隣接リスト

    private Dictionary<int, bool> _dic = new Dictionary<int, bool>();//Button用

    private void Awake()
    {
        _skillList = new List<int>[_buttons.Length];

        for (int i = 0; i < _buttons.Length; i++)
        {
            int count = i;
            _dic.Add(i, false);
            _buttons[i].onClick.AddListener(() => OnPush(count));//イベントを追加
            _buttons[i].interactable = _dic[i]; //Buttonを押せないようにする
            _skillList[i] = new List<int>(); // 各頂点に空の隣接リストを初期化
        }

        //0番目だけ最初から開放できるようにする
        _buttons[0].interactable = true;

        ReadTreeData();
        DebugTest();
    }

    /// <summary>
    /// スキルを開放して子のスキルを開放できるようにする
    /// </summary>
    public void OnPush(int index)
    {
        //スキルポイントが足りてたらスキルを開放できる
        if (_skills[index].OnSkill())
        {
            //押されたButtonの子を開放できるようにする
            foreach (int n in _skillList[index])
            {
                _dic[n] = true;
                _buttons[n].interactable = _dic[n];
            }

            _panels[index].SetActive(true);//二度押せないようにパネルを出す
        }
    }

    /// <summary>
    /// TextAseetからデータを読み取ってきて隣接リストに追加する
    /// </summary>
    private void ReadTreeData()
    {
        StringReader reader = new StringReader(_skillTreeSheet.text);
        reader.ReadLine();//最初の行は説明が書いてあるので読み飛ばす

        while (true)
        {
            string treeLine = reader.ReadLine();

            //nullか空文字だったらtrueを返す
            if (string.IsNullOrEmpty(treeLine))
            {
                break;
            }

            var treeDates = treeLine.Split(",");
            int edge = int.Parse(treeDates[0]);

            //頂点のリストに子の頂点を追加する
            for (int i = 1; i < treeDates.Length; i++)
            {
                if (treeDates[i] == "") break;
                _skillList[edge].Add(int.Parse(treeDates[i]));
            }
        }
    }

    /// <summary>
    /// デバッグ用のメソッド
    /// </summary>
    private void DebugTest()
    {
        for (int i = 0; i < _skillList.Length; i++)
        {
            Debug.Log($"頂点{i}: ");
            foreach (int j in _skillList[i])
            {
                Debug.Log(j);
            }
        }
    }
}