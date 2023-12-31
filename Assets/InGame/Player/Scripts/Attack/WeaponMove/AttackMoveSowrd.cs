using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackMoveSowrd : WeaponAttackMoveBase
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

    private bool _isAfterImage = false;

    /// <summary>戦闘で武器を出した時の処理</summary>
    public override void SelectWeapon()
    {

    }

    /// <summary>戦闘で武器をしまった時の処理</summary>
    public override void UseEndWeapon()
    {

    }


    public override void AttackAir()
    {

    }


    /// <summary>攻撃に入って一番最初の処理</summary>
    public override void AttackFirst()
    {
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
        if ((!_playerControl.Attack.IsStartGround && _thisWeaponAttackCount >= 3) || _thisWeaponAttackCount >= 5)
        {
            _thisWeaponAttackCount = 0;
            _playerControl.Attack.IsCanAttack = false;
        }

        //空中で十分近づいている、地面での攻撃、は即座に攻撃する
        if (_playerControl.AttackAsist.AttackAssistAir(_playerControl.Attack.AttackStartPos, _asisstMaxDistanceAir) && !_playerControl.Attack.IsStartGround
            || _playerControl.Attack.IsStartGround || _playerControl.EnemyCheck.NowEnemy == null)
        {
            _playerControl.Animator.SetInteger("AttackType", 1);
            _playerControl.AnimControl.AttackAnim();
        }
        else
        {
            _isAfterImage = false;

            //アニメーターのパラメータ設定
            _playerControl.AnimControl.AssistSet(true);

            //アニメーションの再生
            _playerControl.AnimControl.PlayerAnimPlay(_playerControl.AttackAsist.AnimName);

            //武器を消す
            _playerControl.WeaponSetting.WeaponActives(false);

            //プレイヤーの透明にする
            _playerControl.MaterialControl.ChangeMaterial(MaterialControl.PlayerMaterialType.Clear);

            //残像を出す
            _playerControl.AfterImage.PlayAfterImage(_playerControl.AttackAsist.AnimName, _playerControl.PlayerT.position, _playerControl.AttackAsist.TargetRotatiom);

            //パーティクルを発生
            _playerControl.ParticleControl.WarpParticle(true);
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


    /// <summary>Attack中に回すUpdata</summary>
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

                    _playerControl.Rb.velocity = Vector3.zero;
                    _isAttack = true;

                    //武器を出す
                    _playerControl.WeaponSetting.WeaponActives(true);

                    //プレイヤーのマテリアルを元に戻す
                    _playerControl.MaterialControl.ChangeMaterial(MaterialControl.PlayerMaterialType.Nomal);

                    //パーティクルを止める
                    _playerControl.ParticleControl.WarpParticle(false);

                    _playerControl.AnimControl.AssistSet(false);
                    _playerControl.Animator.SetInteger("AttackType", 1);
                    _playerControl.AnimControl.AttackAnim();
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

    public void AirAttack5(bool isStart)
    {
        if (isStart)
        {
            _playerControl.Rb.velocity = Vector3.down * _speedDown;
        }
        else
        {
            _playerControl.Avoid.IsCanNotAvoid = false;
        }
    }


    public override void ReSetValue()
    {
        _thisWeaponAttackCount = 0;
    }

    public override void EndAttack()
    {
        _isAttack = false;

        //プレイヤーのマテリアルを元に戻す
        _playerControl.MaterialControl.ChangeMaterial(MaterialControl.PlayerMaterialType.Nomal);

        //パーティクルを消す
        _playerControl.ParticleControl.WarpParticle(false);
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

        //パーティクルを止める
        _playerControl.ParticleControl.WarpParticle(false);
    }
}
