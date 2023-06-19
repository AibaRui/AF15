using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "Weapon_Sword")]
public class ScritableWeaponSword : ScritableWeaponData
{
    [Header("�ʏ�U���̃G�t�F�N�g")]
    [SerializeField] private List<GameObject> _effects = new List<GameObject>();

    [Header("�V�t�g�u���C�N�U���̃G�t�F�N�g")]
    [SerializeField] private GameObject _shiftBreakEffects;

    public List<GameObject> Efects => _effects;

    public GameObject ShiftBreakEffect => _shiftBreakEffects;

}
