using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAttackMoveBase
{
    protected int _thisWeaponAttackCount = 0;

    protected bool _isAttack = false;

    public int AttackCount { get => _thisWeaponAttackCount; set => _thisWeaponAttackCount = value; }

    protected PlayerControl _playerControl;

    public void InitializeWeapon(PlayerControl playerControl)
    {
        if (_playerControl == null)
        {
            _playerControl = playerControl;
        }

    }


    /// <summary>�퓬�ŕ�������܂������̏���</summary>  
    public abstract void UseEndWeapon();

    /// <summary>�퓬�ŕ�����o�������̏���</summary>
    public abstract void SelectWeapon();

    /// <summary>�U������</summary>
    public abstract void AttackFirst();

    /// <summary>�_���[�W���󂯂����̒��f����</summary>
    public abstract void StopAttackForDamage();

    public abstract void AttackAir();

    public abstract void ReSetValue();

    public abstract void WeaponUpData();

    public abstract void WeaponFixedUpdata();

    public abstract void EndAttack();

    public abstract void ShiftBreakAttack();

    public abstract void ShiftBreakAttackEnd();

    public void ReSetAttackCount()
    {
        _thisWeaponAttackCount = 0;
    }

}
