using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWeapons : MonoBehaviour
{
    [Header("=====剣のデータ=====")]
    [SerializeField] private WeaponDataSword _swordData;

    [Header("=====大剣のデータ=====")]
    [SerializeField] private WeaponDataLargeSword _largeSwordData;

    [Header("=====双剣のデータ=====")]
    [SerializeField] private WeaponDataTwinSword _twinSwordData;

    [Header("======槍のデータ======")]
    [SerializeField] private WeaponDataSpear _spearData;

    [Header("======銃のデータ======")]
    [SerializeField] private WeaponDataGun _gunData;

    [SerializeField] private PlayerControl _playerControl;

    [Header("攻撃範囲の設定")]
    [SerializeField] private AttackRange _attackRange;

    public WeaponDataSword WeaponDataSword => _swordData;

    public WeaponDataLargeSword WeaponDataLargeSword => _largeSwordData;
    public WeaponDataTwinSword WeaponDataTwinSword => _twinSwordData;

    public WeaponDataSpear WeaponDataSpear => _spearData;

    public WeaponDataGun WeaponDataGun => _gunData;


    public AttackRange AttackRange => _attackRange;

    public PlayerControl PlayerControl => _playerControl;

    private void Awake()
    {
        _swordData.Init(this);
        _largeSwordData.Init(this);
        _twinSwordData.Init(this);
        _gunData.Init(this);
        _spearData.Init(this);

        _attackRange.Init(this);
    }

    public enum WeaponType
    {
        Panisssher,

        /// <summary>剣</summary>
        Sword,
        /// <summary>槍</summary>
        Spear,
        /// <summary>大剣</summary>
        LargeSword,
        /// <summary>双剣</summary>
        twinSword,

        Gun,
    }

    public WeaponDataBase GetWeaponData(WeaponType type)
    {
        if (type == WeaponType.Sword)
        {
            return _swordData;
        }
        else if (type == WeaponType.Spear)
        {
            return _spearData;
        }
        else if (type == WeaponType.LargeSword)
        {
            return _largeSwordData;
        }
        else if (type == WeaponType.twinSword)
        {
            return _twinSwordData;
        }
        else if (type == WeaponType.Gun)
        {
            return _gunData;
        }
        return _swordData;

    }

    public WeaponAttackMoveBase WeaponGetActionData(WeaponType type)
    {
        if (type == WeaponType.Sword)
        {
            return _swordData.AttackMoveSowrd;
        }
        else if (type == WeaponType.Spear)
        {
            return _spearData.AttackMoveSpear;
        }
        else if (type == WeaponType.LargeSword)
        {
            return _largeSwordData.AtackMoveLarfeSword;
        }
        else if (type == WeaponType.twinSword)
        {
            return _twinSwordData.AttackMoveTwinSword;
        }
        else if (type == WeaponType.Gun)
        {
            return _gunData.AttackMoveGun;
        }
        return _swordData.AttackMoveSowrd;

    }

    private void OnDrawGizmosSelected()
    {
        _attackRange.OnDrowGizmo(_playerControl.PlayerT);
    }

    void Start()
    {
        //_playerControl.WeaponSetting.NowWeapon = _panissher;
    }

    void Update()
    {

    }



}

/// <summary>武器の属性を示す列挙型</summary>
public enum Attribute
{
    /// <summary>ノーマル</summary>
    Nomal,
    /// <summary>火属性 </summary>
    Fire,
    /// <summary>氷属性</summary>
    Ice,
    /// <summary>雷属性</summary>
    Thunder,
}