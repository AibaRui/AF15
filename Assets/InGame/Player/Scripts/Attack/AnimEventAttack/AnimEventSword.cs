using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventSword : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;

    WeaponAttackMoveBase _weaponBase;


    public void SwordAirStart5()
    {
        _playerControl.AllWeapons.WeaponDataSword.AttackMoveSowrd.AirAttack5(true);
    }

    public void SwordAirEnd5()
    {
        _playerControl.AllWeapons.WeaponDataSword.AttackMoveSowrd.AirAttack5(false);
    }


}
