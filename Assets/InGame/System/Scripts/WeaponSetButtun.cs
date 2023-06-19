using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSetButtun : MonoBehaviour
{
    [Header("武器のデータ")]
    private ScritableWeaponData _weaponData;

    [Header("武器の名前のText")]
    [SerializeField] private Text _nameText;

    [Header("武器の攻撃力")]
    [SerializeField] private Text _attackPowerText;

    [Header("武器のアイコン")]
    [SerializeField] private Image _weaponIcon;

    [Header("属性のアイコン")]
    [SerializeField] private Image _attributeIcon;

    private WeaponSetUIControl _weaponSetUIControl;

    public Text NameText => _nameText;
    public Text AttackPowerText => _attackPowerText;
    public Image WeaponIcon => _weaponIcon;
    public Image AttributeIcon => _attributeIcon;

    public ScritableWeaponData ScritableWeaponData { get => _weaponData; set => _weaponData = value; }

    public void Init(ScritableWeaponData weaponData, WeaponSetUIControl uIControl)
    {
        _weaponSetUIControl = uIControl;
        _weaponData = weaponData;

        _nameText.text = _weaponData.WeaponName;
        _attackPowerText.text = _weaponData.AttackPower.ToString();

        _weaponIcon.sprite = uIControl.GetIconData(_weaponData.WeaponType);
        _attributeIcon.sprite = uIControl.GetAttribureIcon(_weaponData.Attribute);
    }

    public void SetWeapon()
    {
        PlayerInformationManager.Instance.WeaponInventoryControl.SetWeapon(_weaponData);
    }

    public void CloseWeaponPanel()
    {
        _weaponSetUIControl.PanelStack.StackPanel.Pop().SetActive(false);
    }

}
