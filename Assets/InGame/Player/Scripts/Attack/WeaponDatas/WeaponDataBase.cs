using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class WeaponDataBase
{
    [Header("�V�t�g�u���C�N�̐ݒ�")]
    [SerializeField] private ShiftBreakSetting _shiftBreakSetting;

    [Header("��������o���Ƃ��̃A�j���[�V�����̖��O")]
    [SerializeField] private string _settingAnimationName;

    public string SettingAnimName => _settingAnimationName;
    public ShiftBreakSetting ShiftBreakSetting => _shiftBreakSetting;
}
