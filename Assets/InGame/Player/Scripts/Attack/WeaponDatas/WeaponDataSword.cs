using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponDataSword : WeaponDataBase
{
    [Header("攻撃に関する設定")]
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
