using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class WeaponDataBase
{
    [Header("シフトブレイクの設定")]
    [SerializeField] private ShiftBreakSetting _shiftBreakSetting;

    [Header("武器を取り出すときのアニメーションの名前")]
    [SerializeField] private string _settingAnimationName;

    public string SettingAnimName => _settingAnimationName;
    public ShiftBreakSetting ShiftBreakSetting => _shiftBreakSetting;
}
