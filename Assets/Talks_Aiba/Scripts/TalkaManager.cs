using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TalkaManager : MonoBehaviour
{
    [Header("会話の内容のスクリタブルオブジェクト")]
    [SerializeField] private TalkData _talkData;

    [Header("開始時に行いたい処理")]
    [SerializeField] private UnityEvent _startTalk;

    [Header("終了時に行いたい処理")]
    [SerializeField] private UnityEvent _endTalk;

    [Header("話すUI")]
    [SerializeField] private GameObject _talkPanel;

    [Header("話し手の名前")]
    [SerializeField] private Text _nameText;

    [Header("会話のText")]
    [SerializeField] private Text _talkText;

    [Header("会話可能アシスト")]
    [SerializeField] private GameObject _talkAssist;

    [SerializeField]
    private GameObject _talker;

    private Collider _collider;



    private List<string> _talk = new List<string>();

    private bool _isEnter = false;

    private bool _isTalkNow = false;
    public bool IsEnter { get => _isEnter; set => _isEnter = value; }
    public Collider Collider { get => _collider; set => _collider = value; }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isTalkNow && _isEnter)
        {
            StartTalk();
        }
    }

    private void FixedUpdate()
    {
        if(_collider!=null)
        {
            _talker.transform.forward = _collider.gameObject.transform.position;
        }
    }

    /// <summary>
    /// 会話を始める
    /// </summary>
    public void StartTalk()
    {
        //開始時に呼びたい処理を呼ぶ
        _startTalk?.Invoke();

        //会話のパネルを表示。
        _talkPanel.SetActive(true);

        _isTalkNow = true;

        StartCoroutine(Talk());

        SetAssistPanel(false);
    }

    /// <summary>
    /// 会話の処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Talk()
    {
        //ミッションの受付状態によって、会話の内容を変える

        _nameText.text = _talkData.HumanName;

        List<string> talk = new List<string>();
        talk = _talkData.DataTalk;

        //話しているテキストを更新。
        for (int i = 0; i < talk.Count; i++)
        {
            //テキストを更新
            _talkText.text = talk[i];
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        //会話のパネルを閉じる
        EndTalk();
        StopCoroutine(Talk());
    }
    public void EndTalk()
    {

        //終了処理
        _endTalk?.Invoke();

        //会話のパネルを非表示にする
        _talkPanel.SetActive(false);

        _isTalkNow = false;
        _isEnter = false;
    }


    public void SetAssistPanel(bool isOn)
    {
        _talkAssist.SetActive(isOn);
    }

}
