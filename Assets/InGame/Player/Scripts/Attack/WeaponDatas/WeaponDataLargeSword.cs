using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDataLargeSword : WeaponDataBase
{
    [Header("攻撃に関する設定")]
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
