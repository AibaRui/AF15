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


    /// <summary>戦闘で武器をしまった時の処理</summary>  
    public abstract void UseEndWeapon();

    /// <summary>戦闘で武器を出した時の処理</summary>
    public abstract void SelectWeapon();

    /// <summary>攻撃処理</summary>
    public abstract void AttackFirst();

    /// <summary>ダメージを受けた時の中断処理</summary>
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
