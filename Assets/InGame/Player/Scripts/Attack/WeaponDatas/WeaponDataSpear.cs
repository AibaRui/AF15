using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDataSpear : WeaponDataBase
{
    [Header("攻撃に関する設定")]
    [SerializeField] private AttackMoveSpear _spear;

     
    public AttackMoveSpear AttackMoveSpear => _spear;

    private AllWeapons _allWeapons;

    public AllWeapons AllWeapons => _allWeapons;

    public void Init(AllWeapons allWeapon)
    {
        _allWeapons = allWeapon;
        _spear.InitializeWeapon(allWeapon.PlayerControl);
    }
}
