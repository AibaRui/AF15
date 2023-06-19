using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponPanissher : WeaponAttackMoveBase
{
    [Header("�e")]
    [SerializeField] private GameObject _bullet;

    [Header("�}�Y��")]
    [SerializeField] private Transform _muzzle;

    /// <summary>�퓬�ŕ�����o�������̏���</summary>
    public override void SelectWeapon()
    {

    }

    /// <summary>�퓬�ŕ�������܂������̏���</summary>
    public override void UseEndWeapon()
    {

    }

    /// <summary>�U������</summary>
    public override void AttackFirst()
    {
        _playerControl.Attack.IsAttackNow = true;

        _playerControl.Animator.SetInteger("AttackType", 0);
        _playerControl.AnimControl.AttackAnim();

        //var go = _playerControl.InsGameObject(_bullet);
        //go.transform.position = _muzzle.position;
        //go.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward;
    }


    public override void WeaponUpData()
    {
        if (_playerControl.InputManager.IsAttack)
        {
            AttackFirst();
        }
    }

    public override void ReSetValue()
    {

    }

    public override void AttackAir()
    {

    }

    public override void EndAttack()
    {

    }

    public override void WeaponFixedUpdata()
    {
        throw new System.NotImplementedException();
    }

    public override void ShiftBreakAttack()
    {

    }

    public override void ShiftBreakAttackEnd()
    {
        throw new System.NotImplementedException();
    }

    public override void StopAttackForDamage()
    {
        throw new System.NotImplementedException();
    }
}
