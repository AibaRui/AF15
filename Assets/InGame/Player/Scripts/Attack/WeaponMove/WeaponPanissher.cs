using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponPanissher : WeaponAttackMoveBase
{
    [Header("’e")]
    [SerializeField] private GameObject _bullet;

    [Header("ƒ}ƒYƒ‹")]
    [SerializeField] private Transform _muzzle;

    /// <summary>í“¬‚Å•Ší‚ğo‚µ‚½‚Ìˆ—</summary>
    public override void SelectWeapon()
    {

    }

    /// <summary>í“¬‚Å•Ší‚ğ‚µ‚Ü‚Á‚½‚Ìˆ—</summary>
    public override void UseEndWeapon()
    {

    }

    /// <summary>UŒ‚ˆ—</summary>
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
