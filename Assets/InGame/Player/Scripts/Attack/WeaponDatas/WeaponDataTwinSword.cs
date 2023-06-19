using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDataTwinSword : WeaponDataBase
{
    [Header("UŒ‚‚ÉŠÖ‚·‚éÝ’è")]
    [SerializeField] private AttackMoveTwinSowrd _twinSowrld;


    public AttackMoveTwinSowrd AttackMoveTwinSword => _twinSowrld;

    private AllWeapons _allWeapons;

    public AllWeapons AllWeapons => _allWeapons;

    public void Init(AllWeapons allWeapon)
    {
        _allWeapons = allWeapon;
        _twinSowrld.InitializeWeapon(allWeapon.PlayerControl);
    }
}
