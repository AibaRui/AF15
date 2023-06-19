using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack : IPlayerAction
{

    [Header("クールタイム")]
    [SerializeField] private float _coolTime = 3;

    private int _attackCount = 0;

    private float _coolTimeCount = 0;

    private Collider[] _enemys;


    private bool _isNowAttack = false;

    private bool _isPushAttack = false;

    private Vector3 _attackStartPos;

    private bool _isStartGround = false;

    /// <summary>攻撃可能かどうか</summary>
    private bool _isCanAttack = true;

    /// <summary>次の攻撃に移行可能かどうか</summary>
    private bool _isCanNextAttack = false;

    private bool _isContinuousAttack = false;

    public bool IsStartGround => _isStartGround;

    public bool IsContinuousAttack { get => _isContinuousAttack; set => _isContinuousAttack = value; }

    public bool IsPushAttack { get => _isPushAttack; set => _isPushAttack = value; }

    public bool IsCanAttack { get => _isCanAttack; set => _isCanAttack = value; }

    public bool IsCanNextAttack { get => _isCanNextAttack; set => _isCanNextAttack = value; }

    public bool IsAttackNow { get => _isNowAttack; set => _isNowAttack = value; }

    public int AttackCount { get => _attackCount; set => _attackCount = value; }

    public Vector3 AttackStartPos => _attackStartPos;

    public void AttackCoolTime()
    {
        if (!_isCanAttack)
        {
            _coolTimeCount += Time.deltaTime;

            if (_coolTimeCount > _coolTime)
            {
                _coolTimeCount = 0;
                _isCanAttack = true;
            }
        }
    }

    public void AttackInputedCheck()
    {
        if (_playerControl.InputManager.IsAttack)
        {
            _isPushAttack = true;
        }
    }

    /// <summary>Updata</summary>
    public void AttackUpdata()
    {
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).WeaponUpData();
    }

    /// <summary>Updata</summary>
    public void AttackFixedUpdata()
    {
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).WeaponFixedUpdata();
    }

    public void AttackStartSet()
    {
        _isPushAttack = false;

        _isNowAttack = true;
    }

    /// <summary>攻撃し始めの処理</summary>
    public void AttackFirst()
    {
        if(_playerControl.WeaponSetting.NowWeapon.WeaponType == AllWeapons.WeaponType.Gun)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Fire);
        }
        else
        {
            _playerControl.SoundManager.PlaySound(AudioType.Attack);
        }

        _isPushAttack = false;

        _isStartGround = _playerControl.GroundCheck.IsHit();

        _attackStartPos = _playerControl.PlayerT.position;

        //攻撃回数の設定
        _attackCount++;

        //武器の変更を確認
        _playerControl.WeaponSetting.CheckWeapon();

        //武器を出す
        _playerControl.WeaponSetting.WeaponActives(true);

        //攻撃処理
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).AttackFirst();

        //武器を選択した時の処理　　????
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).SelectWeapon();

        //武器の攻撃範囲の設定
        _playerControl.AllWeapons.AttackRange.SetRange(_playerControl.WeaponSetting.NowWeapon.WeaponType);
    }

    /// <summary>攻撃終わりの処理</summary>
    public void EndAttack()
    {
        _isPushAttack = false;

        _playerControl.Attack.IsCanNextAttack = false;

        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).UseEndWeapon();

        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).EndAttack();
    }

    public void ReSetAttackCount()
    {
        _playerControl.AnimControl.SetAttackCount(0);
        _attackCount = 0;

        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).AttackCount = 0;
    }

    public void Damage()
    {
        ReSetAttackCount();
        _isPushAttack = false;
        _isNowAttack = false;

    }

}


