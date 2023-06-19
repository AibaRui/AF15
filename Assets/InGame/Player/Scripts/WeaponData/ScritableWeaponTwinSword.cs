using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon_TwinSword")]
public class ScritableWeaponTwinSword : ScritableWeaponData
{
    [Header("‚à‚¤ˆê‚Â‚Ì’ZŒ•")]
    [SerializeField] private GameObject _twinSowrd;

    public GameObject TwinSword => _twinSowrd;
}
