using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameWeaponSelect : MonoBehaviour
{

    [Header("�C���Q�[���̑S�Ă�UI")]
    [SerializeField] private GameObject _allPanel;

    [Header("�����ݒ�{�^����Image")]
    [SerializeField] private List<Image> _setWeaponButtunImages = new List<Image>();

    [Header("�I��p�A�C�R��")]
    [SerializeField] private RectTransform _icon;

    [Header("�I��p�A�C�R���̂���")]
    [SerializeField] private List<RectTransform> _psos = new List<RectTransform>();


    [Header("===Hp�ݒ�===")]
    [Header("�X���C�_�[�ݒ�")]
    [SerializeField] private Slider _slider;

    [Header("Text�ݒ�")]
    [SerializeField] private Text _hpText;

    [SerializeField] private WeaponSetUIControl _weaponSet;
    [SerializeField] private PlayerControl _playerControl;


    private void Start()
    {
        _slider.maxValue = _playerControl.PlayerStatus.MaxHp;
        _slider.minValue = 0;
        _slider.value = _playerControl.PlayerStatus.MaxHp;
        _hpText.text = $"{_playerControl.PlayerStatus.MaxHp}/{_playerControl.PlayerStatus.MaxHp}";
    }

    public void UIActive(bool isActive)
    {
        _allPanel.SetActive(isActive);
    }

    public void SelectWeaponUI(int num)
    {
        _icon.position = _psos[num].position;
    }

    public void SetIcon(int num, ScritableWeaponData data)
    {
        if (data == null)
        {
            _setWeaponButtunImages[num - 1].sprite = null;
        }
        else
        {
            _setWeaponButtunImages[num - 1].sprite = _weaponSet.GetRealIconData(data.WeaponType);
        }
    }


    public void LifeSet(float hp)
    {
        float setHp = 0;

        if (hp > 0) setHp = hp;
        _hpText.text = $"{setHp}/{_playerControl.PlayerStatus.MaxHp}";



        _slider.value = setHp;

        Debug.Log(hp);
        Debug.Log(_slider.value);
    }

}
