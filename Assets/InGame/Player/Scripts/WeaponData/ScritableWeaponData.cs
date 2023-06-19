using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class ScritableWeaponData : ScriptableObject
{
    [Header("武器の名前")]
    [SerializeField] private string _weaponName;

    [Header("武器の攻撃力")]
    [SerializeField] private int _attackPower;

    [Header("武器のタイプ")]
    [SerializeField] private AllWeapons.WeaponType _weaponType;

    [Header("実際につかう武器のプレハブ")]
    [SerializeField] private GameObject _weaponUseObject;

    [Header("選択の時に出す武器のプレハブ")]
    [SerializeField] private GameObject _weaponLookObject;

    [Header("パラメータ設定")]
    [SerializeField] private Attribute _attribute;


    public Attribute Attribute => _attribute;
    public string WeaponName => _weaponName;

    public AllWeapons.WeaponType WeaponType => _weaponType;

    public GameObject WeaponObject => _weaponUseObject;

    public GameObject WeaponLookObject => _weaponLookObject;

    public int AttackPower => _attackPower;

}
