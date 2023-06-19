using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSetUIControl : MonoBehaviour
{
    [Header("=====�ꗗ�̐ݒ�=====")]

    [Header("����ꗗ�̑�{�̃p�l��")]
    [SerializeField] private GameObject _weaponPanel;

    [Header("�ꗗ�̂̃p�l�B")]
    [SerializeField] private List<GameObject> _weaponPanels = new List<GameObject>();

    [Header("����̃A�C�R���B")]
    [SerializeField] private List<RectTransform> _icons = new List<RectTransform>();

    [Header("�A�C�R��")]
    [SerializeField] private RectTransform _selectIcon;

    [Header("���̈ꗗ")]
    [SerializeField] private Transform _swordPanel;

    [Header("�匕�̈ꗗ")]
    [SerializeField] private Transform _largeSwordPanel;

    [Header("�o���̈ꗗ")]
    [SerializeField] private Transform _twinSwordPanel;

    [Header("���̈ꗗ")]
    [SerializeField] private Transform _spearPanel;

    [Header("�e�̈ꗗ")]
    [SerializeField] private Transform _gunPanel;

    [Header("����̎�ނ̖��O��")]
    [SerializeField] private List<string> _weaponTypeName = new List<string>();

    [Header("����̎�ނ̖��O��Text")]
    [SerializeField] private Text _weaponTypeNameText;

    [Header("����̏ڍ�Text")]
    private Text _weaponInfoText;

    [Header("�{�^���̃x�[�X")]
    [SerializeField] private GameObject _panelbase;

    [SerializeField] private Vector3 _localScale;

    [Header("=====�X�e�[�^�X�A�����ꗗ�̕��̐ݒ�=====")]
    [Header("�����ݒ�{�^����Image")]
    [SerializeField] private List<Image> _setWeaponButtunImages = new List<Image>();
    [Header("�����ݒ�{�^����Text")]
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
        //����̃^�C�v����ύX
        _weaponTypeNameText.text = _weaponTypeName[_nowSelect];
        //�I���A�C�R���̈ʒu����
        _selectIcon.position = _icons[_nowSelect].position;

        foreach (var a in _weaponPanels)
        {
            a.SetActive(false);
        }
        //�p�l����\��
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
                //�p�l����\��
                _weaponPanels[_nowSelect].SetActive(true);
                //����̃^�C�v����ύX
                _weaponTypeNameText.text = _weaponTypeName[_nowSelect];
                //�I���A�C�R���̈ʒu����
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
                //�p�l����\��
                _weaponPanels[_nowSelect].SetActive(true);
                //����̃^�C�v����ύX
                _weaponTypeNameText.text = _weaponTypeName[_nowSelect];
                //�I���A�C�R���̈ʒu����
                _selectIcon.position = _icons[_nowSelect].position;
            }
        }
    }


    /// <summary>����̃p�l�����e����̃p�l���ɒǉ�����</summary>
    public void AddWeaponPanel(ScritableWeaponData data)
    {
        if (data == null) return;

        //�p�l���𐶐�
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

    /// <summary>���A�����̃A�C�R����Ԃ�</summary>
    /// <param name="weaponType">����̎��</param>
    /// <returns>�摜�f�[�^��Ԃ�</returns>
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

    /// <summary>�摜��Ԃ�</summary>
    /// <param name="weaponType">����̎��</param>
    /// <returns>�摜</returns>
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
