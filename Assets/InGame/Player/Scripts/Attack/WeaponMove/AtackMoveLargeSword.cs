using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AtackMoveLargeSword : WeaponAttackMoveBase
{
    [Header("攻撃の降下速度")]
    [SerializeField] private float _speedDown = 20;

    [Header("=== 地上のアシスト設定 ===")]
    [Header("アシスト速度")]
    [SerializeField] private float _asisstSpeedGround = 3;

    [Header("アシスト最大距離")]
    [SerializeField] private float _asisstMaxDistanceGround = 5;

    [Header("2回目以降、アシスト速度")]
    [SerializeField] private float _asisstSpeedGroundSecond = 1;

    [Header("2回目以降、アシスト最大距離")]
    [SerializeField] private float _asisstMaxDistanceGroundSecond = 1;

    [Header("=== 空中のアシスト設定 ===")]
    [Header("アシスト速度")]
    [SerializeField] private float _asisstSpeedAir = 5;

    [Header("アシスト最大距離")]
    [SerializeField] private float _asisstMaxDistanceAir = 10;

    [Header("ターゲットなし時の、アシスト速度")]
    [SerializeField] private float _noAsisstSpeedAir = 1;

    [Header("ターゲットなし時の、アシスト最大距離")]
    [SerializeField] private float _noAsisstMaxDistanceAir = 1;

    /// <summary>設定するアシストの速度</summary>
    private float _setSpeed = 0;

    /// <summary>設定するアシストの距離</summary>
    private float _setDistance = 0;

    private bool _isAttack2 = false;

    /// <summary>戦闘で武器を出した時の処理</summary>
    public override void SelectWeapon()
    {

    }

    /// <summary>戦闘で武器をしまった時の処理</summary>
    public override void UseEndWeapon()
    {

    }

    /// <summary>攻撃処理</summary>
    public override void AttackFirst()
    {
        //現在攻撃中
        _playerControl.Attack.IsAttackNow = true;

        _thisWeaponAttackCount++;


        if (!_playerControl.GroundCheck.IsHit())
        {
            _playerControl.Rb.velocity = Vector3.zero;
            _playerControl.Rb.useGravity = false;
        }

        //攻撃回数の設定（アニメーター）
        _playerControl.AnimControl.SetAttackCount(_thisWeaponAttackCount);

        _playerControl.AttackAsist.AssistSet(_playerControl.EnemyCheck.NowEnemy);

        SetSpeedAndDistance();

        //空中3回目の攻撃で
        if ((!_playerControl.Attack.IsStartGround) || _thisWeaponAttackCount >= 5)
        {
            _thisWeaponAttackCount = 0;
            _playerControl.Attack.IsCanAttack = false;
        }

        _playerControl.Animator.SetInteger("AttackType", 4);
        _playerControl.AnimControl.AttackAnim();

        if (!_playerControl.GroundCheck.IsHit())
        {
            _playerControl.Rb.useGravity = false;
            _playerControl.Rb.velocity = Vector3.zero;
        }

    }

    public void SetSpeedAndDistance()
    {
        //攻撃はじめが地上か空中かどうか
        if (_playerControl.Attack.IsStartGround)
        {
            _setDistance = _thisWeaponAttackCount == 1 ? _asisstMaxDistanceGround : _asisstMaxDistanceGroundSecond;

            //１回目の攻撃は大きくアシスト
            if (_playerControl.EnemyCheck.NowEnemy != null)
            {
                _setSpeed = _thisWeaponAttackCount == 1 ? _asisstSpeedGround : _asisstSpeedGroundSecond;
            }
            else
            {
                _setSpeed = _asisstSpeedGroundSecond;
            }
        }
        else
        {
            _setDistance = _playerControl.EnemyCheck.NowEnemy != null ? _asisstMaxDistanceAir : _noAsisstMaxDistanceAir;

            //１回目の攻撃は大きくアシスト
            if (_playerControl.EnemyCheck.NowEnemy != null)
            {
                _setSpeed = _asisstSpeedAir;
            }
            else
            {
                _setSpeed = _noAsisstSpeedAir;
            }
        }
    }


    public override void WeaponUpData()
    {
        if (!_isAttack)
        {
            //攻撃はじめが地上か空中かどうか
            if (_playerControl.Attack.IsStartGround)
            {
                //ターゲットととの距離を縮め終えたら攻撃処理実行
                if (_playerControl.AttackAsist.AttackAssistGround(_playerControl.Attack.AttackStartPos, _setDistance))
                {
                    _isAttack = true;
                    _playerControl.Rb.velocity = Vector3.zero;
                }
            }
            else
            {
                //ターゲットととの距離を縮め終えたら攻撃処理実行
                if (_playerControl.AttackAsist.AttackAssistAir(_playerControl.Attack.AttackStartPos, _setDistance))
                {
                    _isAttack = true;
                }
            }
        }
    }


    public override void WeaponFixedUpdata()
    {
        if (!_isAttack)
        {
            _playerControl.AttackAsist.AttaclkAssistMove(_setSpeed);
        }
        _playerControl.AttackAsist.AttackAssistRotation();
    }


    public override void ReSetValue()
    {
        _thisWeaponAttackCount = 0;
    }

    public void AttackAir(bool isStart)
    {
        if (isStart)
        {
            _isAttack = true;
            _playerControl.Rb.velocity = Vector3.down * _speedDown;
        }
        else
        {

            _playerControl.Rb.useGravity = true;
            _playerControl.Avoid.IsCanNotAvoid = false;
            _playerControl.Rb.velocity = Vector3.zero;
        }
    }

    public void AttackStart2()
    {
        _isAttack2 = true;
    }

    public void AttackEnd2()
    {
        _isAttack2 = false;

        _playerControl.Rb.velocity = Vector3.zero;
    }

    public override void AttackAir()
    {

    }

    public override void EndAttack()
    {
        _isAttack = false;
    }



    public override void ShiftBreakAttack()
    {

    }

    public override void ShiftBreakAttackEnd()
    {

    }

    public override void StopAttackForDamage()
    {
        _isAttack = false;
    }
}
