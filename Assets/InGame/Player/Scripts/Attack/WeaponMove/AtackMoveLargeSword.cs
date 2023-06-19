using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AtackMoveLargeSword : WeaponAttackMoveBase
{
    [Header("�U���̍~�����x")]
    [SerializeField] private float _speedDown = 20;

    [Header("=== �n��̃A�V�X�g�ݒ� ===")]
    [Header("�A�V�X�g���x")]
    [SerializeField] private float _asisstSpeedGround = 3;

    [Header("�A�V�X�g�ő勗��")]
    [SerializeField] private float _asisstMaxDistanceGround = 5;

    [Header("2��ڈȍ~�A�A�V�X�g���x")]
    [SerializeField] private float _asisstSpeedGroundSecond = 1;

    [Header("2��ڈȍ~�A�A�V�X�g�ő勗��")]
    [SerializeField] private float _asisstMaxDistanceGroundSecond = 1;

    [Header("=== �󒆂̃A�V�X�g�ݒ� ===")]
    [Header("�A�V�X�g���x")]
    [SerializeField] private float _asisstSpeedAir = 5;

    [Header("�A�V�X�g�ő勗��")]
    [SerializeField] private float _asisstMaxDistanceAir = 10;

    [Header("�^�[�Q�b�g�Ȃ����́A�A�V�X�g���x")]
    [SerializeField] private float _noAsisstSpeedAir = 1;

    [Header("�^�[�Q�b�g�Ȃ����́A�A�V�X�g�ő勗��")]
    [SerializeField] private float _noAsisstMaxDistanceAir = 1;

    /// <summary>�ݒ肷��A�V�X�g�̑��x</summary>
    private float _setSpeed = 0;

    /// <summary>�ݒ肷��A�V�X�g�̋���</summary>
    private float _setDistance = 0;

    private bool _isAttack2 = false;

    /// <summary>�퓬�ŕ�����o�������̏���</summary>
    public override void SelectWeapon()
    {

    }

    /// <summary>�퓬�ŕ�������܂������̏���</summary>
    public override void UseEndWeapon()
    {

    }

    /// <summary>�U������</summary>
    public override void AttackFirst()
    {
        //���ݍU����
        _playerControl.Attack.IsAttackNow = true;

        _thisWeaponAttackCount++;


        if (!_playerControl.GroundCheck.IsHit())
        {
            _playerControl.Rb.velocity = Vector3.zero;
            _playerControl.Rb.useGravity = false;
        }

        //�U���񐔂̐ݒ�i�A�j���[�^�[�j
        _playerControl.AnimControl.SetAttackCount(_thisWeaponAttackCount);

        _playerControl.AttackAsist.AssistSet(_playerControl.EnemyCheck.NowEnemy);

        SetSpeedAndDistance();

        //��3��ڂ̍U����
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
        //�U���͂��߂��n�ォ�󒆂��ǂ���
        if (_playerControl.Attack.IsStartGround)
        {
            _setDistance = _thisWeaponAttackCount == 1 ? _asisstMaxDistanceGround : _asisstMaxDistanceGroundSecond;

            //�P��ڂ̍U���͑傫���A�V�X�g
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

            //�P��ڂ̍U���͑傫���A�V�X�g
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
            //�U���͂��߂��n�ォ�󒆂��ǂ���
            if (_playerControl.Attack.IsStartGround)
            {
                //�^�[�Q�b�g�ƂƂ̋������k�ߏI������U���������s
                if (_playerControl.AttackAsist.AttackAssistGround(_playerControl.Attack.AttackStartPos, _setDistance))
                {
                    _isAttack = true;
                    _playerControl.Rb.velocity = Vector3.zero;
                }
            }
            else
            {
                //�^�[�Q�b�g�ƂƂ̋������k�ߏI������U���������s
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
