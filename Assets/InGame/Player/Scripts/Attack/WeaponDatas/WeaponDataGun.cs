using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDataGun : WeaponDataBase
{
    [Header("UŒ‚‚ÉŠÖ‚·‚éÝ’è")]
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
