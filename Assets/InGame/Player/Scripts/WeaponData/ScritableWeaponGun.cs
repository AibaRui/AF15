using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Weapon_Gun")]

public class ScritableWeaponGun : ScritableWeaponData
{
    [Header("Å‘å’e”")]
    [SerializeField] private int _maxBulletNum;

    public int MaxBulletNum => _maxBulletNum;
}
