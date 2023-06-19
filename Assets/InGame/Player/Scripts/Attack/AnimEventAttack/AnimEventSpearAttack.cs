using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventSpearAttack : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;

    WeaponAttackMoveBase _weaponBase;


    public void SpearAttackStart4()
    {
        _playerControl.AllWeapons.WeaponDataSpear.AttackMoveSpear.Attak4(true);
    }

    public void SpearAttackEnd4()
    {
        _playerControl.AllWeapons.WeaponDataSpear.AttackMoveSpear.Attak4(false);
    }

    public void SpearAttackStart5()
    {
        _playerControl.AllWeapons.WeaponDataSpear.AttackMoveSpear.Attack5(true);
    }

    public void SpearAttackEnd5()
    {
        _playerControl.AllWeapons.WeaponDataSpear.AttackMoveSpear.Attack5(false);
    }
}
