using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDataLargeSword : WeaponDataBase
{
    [Header("UŒ‚‚ÉŠÖ‚·‚éÝ’è")]
    [SerializeField] private AtackMoveLargeSword _largeSowrld;


    public AtackMoveLargeSword AtackMoveLarfeSword => _largeSowrld;

    private AllWeapons _allWeapons;

    public AllWeapons AllWeapons => _allWeapons;

    public void Init(AllWeapons allWeapon)
    {
        _allWeapons = allWeapon;
        _largeSowrld.InitializeWeapon(allWeapon.PlayerControl);
    }
}
