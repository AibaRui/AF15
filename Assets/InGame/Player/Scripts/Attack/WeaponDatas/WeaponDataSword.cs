using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponDataSword : WeaponDataBase
{
    [Header("UŒ‚‚ÉŠÖ‚·‚éÝ’è")]
    [SerializeField] private AttackMoveSowrd _sowrld;


    public AttackMoveSowrd AttackMoveSowrd => _sowrld;

    private AllWeapons _allWeapons;

    public AllWeapons AllWeapons => _allWeapons;

    public void Init(AllWeapons allWeapon)
    {
        _allWeapons = allWeapon;
        _sowrld.InitializeWeapon(allWeapon.PlayerControl);
    }
}
