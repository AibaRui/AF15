using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWeapons : MonoBehaviour
{
    [Header("=====���̃f�[�^=====")]
    [SerializeField] private WeaponDataSword _swordData;

    [Header("=====�匕�̃f�[�^=====")]
    [SerializeField] private WeaponDataLargeSword _largeSwordData;

    [Header("=====�o���̃f�[�^=====")]
    [SerializeField] private WeaponDataTwinSword _twinSwordData;

    [Header("======���̃f�[�^======")]
    [SerializeField] private WeaponDataSpear _spearData;

    [Header("======�e�̃f�[�^======")]
    [SerializeField] private WeaponDataGun _gunData;

    [SerializeField] private PlayerControl _playerControl;

    [Header("�U���͈͂̐ݒ�")]
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

        /// <summary>��</summary>
        Sword,
        /// <summary>��</summary>
        Spear,
        /// <summary>�匕</summary>
        LargeSword,
        /// <summary>�o��</summary>
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

/// <summary>����̑����������񋓌^</summary>
public enum Attribute
{
    /// <summary>�m�[�}��</summary>
    Nomal,
    /// <summary>�Α��� </summary>
    Fire,
    /// <summary>�X����</summary>
    Ice,
    /// <summary>������</summary>
    Thunder,
}