using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDataGun : WeaponDataBase
{
    [Header("攻撃に関する設定")]
    [SerializeField] private AttackMoveGun _gun;

    public AttackMoveGun AttackMoveGun => _gun;

    private AllWeapons _allWeapons;

    public AllWeapons AllWeapons => _allWeapons;

    public void Init(AllWeapons allWeapon)
    {
        _allWeapons = allWeapon;
        _gun.InitializeWeapon(allWeapon.PlayerControl);
    }
}
