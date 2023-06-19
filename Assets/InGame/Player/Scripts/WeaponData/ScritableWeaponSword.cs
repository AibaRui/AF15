using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "Weapon_Sword")]
public class ScritableWeaponSword : ScritableWeaponData
{
    [Header("通常攻撃のエフェクト")]
    [SerializeField] private List<GameObject> _effects = new List<GameObject>();

    [Header("シフトブレイク攻撃のエフェクト")]
    [SerializeField] private GameObject _shiftBreakEffects;

    public List<GameObject> Efects => _effects;

    public GameObject ShiftBreakEffect => _shiftBreakEffects;

}
