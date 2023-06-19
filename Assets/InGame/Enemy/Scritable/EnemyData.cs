using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("“G‚Ì–¼‘O")]
    [SerializeField] private string _enemyName;

    [Header("Å‘å‘Ì—Í")]
    [SerializeField] private int _maxHp;

    [Header("UI‚ÌOffset")]
    [SerializeField] private Vector3 _uIOffSet;

    [Header("“G‚Ìã“_‘®«")]
    [SerializeField] private List<Attribute> _weakAttributes = new List<Attribute>();

    [Header("“G‚Ìã“_•Ší")]
    [SerializeField] private List<AllWeapons.WeaponType> _weakWeapons = new List<AllWeapons.WeaponType>();


    [SerializeField] private int _attackPower;


    public List<Attribute> WeakAttributes => _weakAttributes;
    public List<AllWeapons.WeaponType> WeakWeapons => _weakWeapons;

    public string EnemyName => _enemyName;

    public int MaxHp => _maxHp;

    public Vector3 UIOffSet => _uIOffSet;

}
