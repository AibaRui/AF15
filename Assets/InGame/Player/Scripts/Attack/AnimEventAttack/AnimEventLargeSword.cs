using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventLargeSword : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;


    public void LargeSwordAttackStart2()
    {
        _playerControl.AllWeapons.WeaponDataLargeSword.AtackMoveLarfeSword.AttackStart2();
    }

    public void LargeSwordAttackEnd2()
    {
        _playerControl.AllWeapons.WeaponDataLargeSword.AtackMoveLarfeSword.AttackEnd2();
    }


    public void LargeSwordAirAttackStart()
    {
        _playerControl.AllWeapons.WeaponDataLargeSword.AtackMoveLarfeSword.AttackAir(true);
    }

    public void LargeSwordAirAttackEnd()
    {
        _playerControl.AllWeapons.WeaponDataLargeSword.AtackMoveLarfeSword.AttackAir(false);
    }

}
