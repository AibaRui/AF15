using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class WeaponBow : WeaponAttackMoveBase
{

    [Header("�|�p�̋O��")]
    [SerializeField] private LineRenderer _bowLine;

    /// <summary>�U���̋O�Ղ����������ǂ���</summary>
    private bool _isAttackLine = false;

    public override void AttackAir()
    {

    }

    public override void AttackFirst()
    {
        _playerControl.Attack.IsAttackNow = true;

        _thisWeaponAttackCount++;

        _playerControl.AttackAsist.SetRotation();

        //�U���񐔂̐ݒ�i�A�j���[�^�[�j
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
        //�����\��
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
