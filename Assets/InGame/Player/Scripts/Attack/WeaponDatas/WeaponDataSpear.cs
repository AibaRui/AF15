using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDataSpear : WeaponDataBase
{
    [Header("UŒ‚‚ÉŠÖ‚·‚éÝ’è")]
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
