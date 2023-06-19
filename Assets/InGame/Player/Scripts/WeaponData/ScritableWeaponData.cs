using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class ScritableWeaponData : ScriptableObject
{
    [Header("����̖��O")]
    [SerializeField] private string _weaponName;

    [Header("����̍U����")]
    [SerializeField] private int _attackPower;

    [Header("����̃^�C�v")]
    [SerializeField] private AllWeapons.WeaponType _weaponType;

    [Header("���ۂɂ�������̃v���n�u")]
    [SerializeField] private GameObject _weaponUseObject;

    [Header("�I���̎��ɏo������̃v���n�u")]
    [SerializeField] private GameObject _weaponLookObject;

    [Header("�p�����[�^�ݒ�")]
    [SerializeField] private Attribute _attribute;


    public Attribute Attribute => _attribute;
    public string WeaponName => _weaponName;

    public AllWeapons.WeaponType WeaponType => _weaponType;

    public GameObject WeaponObject => _weaponUseObject;

    public GameObject WeaponLookObject => _weaponLookObject;

    public int AttackPower => _attackPower;

}
