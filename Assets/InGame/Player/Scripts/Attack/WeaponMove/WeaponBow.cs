using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class WeaponBow : WeaponAttackMoveBase
{

    [Header("弓用の軌跡")]
    [SerializeField] private LineRenderer _bowLine;

    /// <summary>攻撃の軌跡をだしたかどうか</summary>
    private bool _isAttackLine = false;

    public override void AttackAir()
    {

    }

    public override void AttackFirst()
    {
        _playerControl.Attack.IsAttackNow = true;

        _thisWeaponAttackCount++;

        _playerControl.AttackAsist.SetRotation();

        //攻撃回数の設定（アニメーター）
        _playerControl.AnimControl.SetAttackCount(_thisWeaponAttackCount);

        _playerControl.Animator.SetInteger("AttackType", 1);
        _playerControl.AnimControl.AttackAnim();

    }

    public override void EndAttack()
    {
        _isAttack = false;

    }

    public override void ReSetValue()
    {
        _thisWeaponAttackCount = 0;
    }

    public override void SelectWeapon()
    {

    }

    public override void ShiftBreakAttack()
    {
        //武器を表示
        _playerControl.WeaponSetting.WeaponActives(true);
        _playerControl.AnimControl.PlayerAnimPlay(_playerControl.AllWeapons.GetWeaponData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ShiftBreakSetting.ShiftBreakAttackAnimName);
    }

    public override void UseEndWeapon()
    {

    }

    public override void WeaponFixedUpdata()
    {
        _playerControl.AttackAsist.AttackAssistRotation();
    }

    public override void WeaponUpData()
    {

    }

    public void BowAttackLine()
    {

    }

    public override void ShiftBreakAttackEnd()
    {
        throw new System.NotImplementedException();
    }
    public override void StopAttackForDamage()
    {
        _isAttack = false;
    }

}
