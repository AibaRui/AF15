using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameWeaponSelect : MonoBehaviour
{

    [Header("インゲームの全てのUI")]
    [SerializeField] private GameObject _allPanel;

    [Header("装備設定ボタンのImage")]
    [SerializeField] private List<Image> _setWeaponButtunImages = new List<Image>();

    [Header("選択用アイコン")]
    [SerializeField] private RectTransform _icon;

    [Header("選択用アイコンのいち")]
    [SerializeField] private List<RectTransform> _psos = new List<RectTransform>();


    [Header("===Hp設定===")]
    [Header("スライダー設定")]
    [SerializeField] private Slider _slider;

    [Header("Text設定")]
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
