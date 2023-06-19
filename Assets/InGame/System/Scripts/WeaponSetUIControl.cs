using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSetUIControl : MonoBehaviour
{
    [Header("=====一覧の設定=====")]

    [Header("武器一覧の大本のパネル")]
    [SerializeField] private GameObject _weaponPanel;

    [Header("一覧ののパネ達")]
    [SerializeField] private List<GameObject> _weaponPanels = new List<GameObject>();

    [Header("武器のアイコン達")]
    [SerializeField] private List<RectTransform> _icons = new List<RectTransform>();

    [Header("アイコン")]
    [SerializeField] private RectTransform _selectIcon;

    [Header("剣の一覧")]
    [SerializeField] private Transform _swordPanel;

    [Header("大剣の一覧")]
    [SerializeField] private Transform _largeSwordPanel;

    [Header("双剣の一覧")]
    [SerializeField] private Transform _twinSwordPanel;

    [Header("槍の一覧")]
    [SerializeField] private Transform _spearPanel;

    [Header("銃の一覧")]
    [SerializeField] private Transform _gunPanel;

    [Header("武器の種類の名前の")]
    [SerializeField] private List<string> _weaponTypeName = new List<string>();

    [Header("武器の種類の名前のText")]
    [SerializeField] private Text _weaponTypeNameText;

    [Header("武器の詳細Text")]
    private Text _weaponInfoText;

    [Header("ボタンのベース")]
    [SerializeField] private GameObject _panelbase;

    [SerializeField] private Vector3 _localScale;

    [Header("=====ステータス、装備一覧の方の設定=====")]
    [Header("装備設定ボタンのImage")]
    [SerializeField] private List<Image> _setWeaponButtunImages = new List<Image>();
    [Header("装備設定ボタンのText")]
    [SerializeField] private List<Text> _setWeaponButtunText = new List<Text>();

    [SerializeField] private ScritableInventorySpriteData _spriteData;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PanelStack _stackPanel;
    [SerializeField] private ItemGetUIControl _itemGetUI;



    private int _nowSelect = 0;

    public PanelStack PanelStack => _stackPanel;
    public ScritableInventorySpriteData SpriteData => _spriteData;

    void Start()
    {
        //武器のタイプ名を変更
        _weaponTypeNameText.text = _weaponTypeName[_nowSelect];
        //選択アイコンの位置調整
        _selectIcon.position = _icons[_nowSelect].position;

        foreach (var a in _weaponPanels)
        {
            a.SetActive(false);
        }
        //パネルを表示
        _weaponPanels[_nowSelect].SetActive(true);
    }

    void Update()
    {
        if (_weaponPanel.activeSelf)
        {
            if (_inputManager.IsInventoryRight)
            {
                _weaponPanels[_nowSelect].SetActive(false);
                _nowSelect++;

                if (_nowSelect >= _weaponPanels.Count)
                {
                    _nowSelect = 0;
                }
                //パネルを表示
                _weaponPanels[_nowSelect].SetActive(true);
                //武器のタイプ名を変更
                _weaponTypeNameText.text = _weaponTypeName[_nowSelect];
                //選択アイコンの位置調整
                _selectIcon.position = _icons[_nowSelect].position;
            }
            else if (_inputManager.IsInventoryLeft)
            {
                _weaponPanels[_nowSelect].SetActive(false);
                _nowSelect--;

                if (_nowSelect < 0)
                {
                    _nowSelect = _weaponPanels.Count - 1;
                }
                //パネルを表示
                _weaponPanels[_nowSelect].SetActive(true);
                //武器のタイプ名を変更
                _weaponTypeNameText.text = _weaponTypeName[_nowSelect];
                //選択アイコンの位置調整
                _selectIcon.position = _icons[_nowSelect].position;
            }
        }
    }


    /// <summary>武器のパネルを各武器のパネルに追加する</summary>
    public void AddWeaponPanel(ScritableWeaponData data)
    {
        if (data == null) return;

        //パネルを生成
        var go = Instantiate(_panelbase);
        go.GetComponent<WeaponSetButtun>().Init(data, this);

        if (data.WeaponType == AllWeapons.WeaponType.Sword)
        {
            go.transform.SetParent(_swordPanel);
        }
        else if (data.WeaponType == AllWeapons.WeaponType.LargeSword)
        {
            go.transform.SetParent(_largeSwordPanel);
        }
        else if (data.WeaponType == AllWeapons.WeaponType.twinSword)
        {
            go.transform.SetParent(_twinSwordPanel);
        }
        else if (data.WeaponType == AllWeapons.WeaponType.Spear)
        {
            go.transform.SetParent(_spearPanel);
        }
        else
        {
            go.transform.SetParent(_gunPanel);
        }
        go.transform.localScale = _localScale;
    }

    public void SetWeaponButtunChangeImage(int num, ScritableWeaponData data)
    {
        if (data == null)
        {
            _setWeaponButtunImages[num - 1].sprite = null;
            _setWeaponButtunText[num - 1].text = "";
        }
        else
        {
            _setWeaponButtunImages[num - 1].sprite = GetRealIconData(data.WeaponType);
            _setWeaponButtunText[num - 1].text = data.WeaponName;
        }
    }

    public Sprite GetAttribureIcon(Attribute attribute)
    {
        if(attribute == Attribute.Fire)
        {
            return _spriteData.FireIcon;
        }
        else if (attribute == Attribute.Ice)
        {
            return _spriteData.IceIcon;
        }
        else if (attribute == Attribute.Thunder)
        {
            return _spriteData.ThenderIcon;
        }
        else
        {
            return null;
        }
    }

    /// <summary>リアル調のアイコンを返す</summary>
    /// <param name="weaponType">武器の種類</param>
    /// <returns>画像データを返す</returns>
    public Sprite GetRealIconData(AllWeapons.WeaponType weaponType)
    {
        if (weaponType == AllWeapons.WeaponType.Sword)
        {
            return _spriteData.SwordIconReal;
        }
        else if (weaponType == AllWeapons.WeaponType.LargeSword)
        {
            return _spriteData.LargeSwordIconReal;
        }
        else if (weaponType == AllWeapons.WeaponType.twinSword)
        {
            return _spriteData.TwinSwordIconReal;
        }
        else if (weaponType == AllWeapons.WeaponType.Spear)
        {
            return _spriteData.SpearRealIcon;
        }
        else
        {
            return _spriteData.GunRealIcon;
        }
    }

    /// <summary>画像を返す</summary>
    /// <param name="weaponType">武器の種類</param>
    /// <returns>画像</returns>
    public Sprite GetIconData(AllWeapons.WeaponType weaponType)
    {
        if (weaponType == AllWeapons.WeaponType.Sword)
        {
            return _spriteData.SwordIcon;
        }
        else if (weaponType == AllWeapons.WeaponType.LargeSword)
        {
            return _spriteData.LargeSwordIcon;
        }
        else if (weaponType == AllWeapons.WeaponType.twinSword)
        {
            return _spriteData.TwinSwordIcon;
        }
        else if (weaponType == AllWeapons.WeaponType.Spear)
        {
            return _spriteData.SpearIcon;
        }
        else
        {
            return _spriteData.GunIcon;
        }
    }


}
