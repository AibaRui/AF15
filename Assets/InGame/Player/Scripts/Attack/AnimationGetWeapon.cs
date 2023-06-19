using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGetWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSetting _weaponSetting;

    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private ItemGetUIControl _itemGetUIControl;

    public void GetWeapon()
    {
        _weaponSetting.WeaponActives(true);
    }


    public void EndAnim()
    {
        _weaponSetting.NowChangeWeaponAnimation = false;
    }

    public void CanNotAvoid()
    {
        _playerControl.Avoid.IsCanNotAvoid = true;
    }


    public void OnWeaponTrailRenderer()
    {
        _playerControl.WeaponSetting.NowUseWeaponObject.GetComponent<ITrailable>()?.UseTrail(true);
    }

    public void OffWeapoTrailRenderer()
    {
        _playerControl.WeaponSetting.NowUseWeaponObject.GetComponent<ITrailable>()?.UseTrail(false);
    }

    public void UseWeaponEffect()
    {
        _playerControl.WeaponSetting.NowUseWeaponObject.GetComponent<IEffectable>()?.UseEffect();
    }


    public void CanNextAttack()
    {
        _playerControl.Rb.useGravity = true;
        _playerControl.Attack.IsCanNextAttack = true;
        _playerControl.AnimControl.CaneNextAttack(true);
    }

    public void AttackHitCheck()
    {
        _playerControl.AllWeapons.AttackRange.AttackHitCheck();
    }

    public void ShiftBreakAttackEnd()
    {
        _playerControl.ShiftBreak.ShiftBreakAttackEnd();
    }

    public void SrowWeaponShiftBreak()
    {
        _playerControl.ShiftBreak.NowThrowWeapon.SetActive(true);
    }

    public void EndShiftBreak()
    {
        _playerControl.ShiftBreak.IsShiftBreake = false;
    }

    public void EndDamageGetUp()
    {
        _playerControl.Damage.GetUpEnd();
    }

    public void PickUpItem()
    {
        _playerControl.PickUp.EndPickUp();
        _itemGetUIControl.OffGetItemPanel();
    }


}
