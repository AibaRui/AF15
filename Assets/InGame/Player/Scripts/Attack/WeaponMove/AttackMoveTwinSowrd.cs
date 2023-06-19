using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackMoveTwinSowrd : WeaponAttackMoveBase
{
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

    private string _nowWeaponName;

    private Dictionary<string, GameObject> _sword = new Dictionary<string, GameObject>();

    public override void AttackAir()
    {

    }

    public override void AttackFirst()
    {
        if (_playerControl.WeaponSetting.NowWeapon is ScritableWeaponTwinSword twinSword)
        {
            if (_sword.ContainsKey(twinSword.WeaponName))
            {
                _sword[twinSword.WeaponName].SetActive(true);
            }   //������̕����\��
            else
            {
                var go = _playerControl.InstantiateObject(twinSword.TwinSword);
                go.transform.SetParent(_playerControl.AnimControl.ModelLeftHandPos);
                go.transform.localPosition = Vector3.zero;
                Quaternion r = Quaternion.Euler(0, 0, 0);
                go.transform.localRotation = r;
                _sword.Add(twinSword.WeaponName, go);
            }   //�o�^����Ă��Ȃ�������A�������ēo�^
        }

        _nowWeaponName = _playerControl.WeaponSetting.NowWeapon.WeaponName;

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
        if ((!_playerControl.Attack.IsStartGround && _thisWeaponAttackCount >= 3) || _thisWeaponAttackCount >= 5)
        {
            _thisWeaponAttackCount = 0;
            _playerControl.Attack.IsCanAttack = false;
        }

        //�󒆂ŏ\���߂Â��Ă���A�n�ʂł̍U���A�͑����ɍU������
        if (_playerControl.AttackAsist.AttackAssistAir(_playerControl.Attack.AttackStartPos, _asisstMaxDistanceAir) && !_playerControl.Attack.IsStartGround
            || _playerControl.Attack.IsStartGround || _playerControl.EnemyCheck.NowEnemy == null)
        {
            _playerControl.Animator.SetInteger("AttackType", 3);
            _playerControl.AnimControl.AttackAnim();
        }
        else
        {
            //�A�j���[�^�[�̃p�����[�^�ݒ�
            _playerControl.AnimControl.AssistSet(true);

            //�A�j���[�V�����̍Đ�
            _playerControl.AnimControl.PlayerAnimPlay(_playerControl.AttackAsist.AnimName);

            //���������
            _playerControl.WeaponSetting.WeaponActives(false);

            //�p�[�e�B�N���𔭐�
            _playerControl.ParticleControl.WarpParticle(true);
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


    public override void EndAttack()
    {

        _isAttack = false;

        //�p�[�e�B�N��������
        _playerControl.ParticleControl.WarpParticle(false);
    }

    public override void ReSetValue()
    {
        _thisWeaponAttackCount = 0;
    }

    public override void SelectWeapon()
    {

    }

    public override void ShiftBreakAttack()
    {
        if (_playerControl.WeaponSetting.NowWeapon is ScritableWeaponTwinSword twinSword)
        {
            if (_sword.ContainsKey(twinSword.WeaponName))
            {
                _sword[twinSword.WeaponName].SetActive(true);
            }   //������̕����\��

        }
    }

    public override void UseEndWeapon()
    {
        if (_playerControl.WeaponSetting.NowWeapon is ScritableWeaponTwinSword twinSword)
        {
            if (!_sword.ContainsKey(_playerControl.WeaponSetting.NowWeapon.WeaponName))
            {
                var go = _playerControl.InstantiateObject(twinSword.TwinSword);
                go.transform.SetParent(_playerControl.AnimControl.ModelLeftHandPos);
                go.transform.localPosition = Vector3.zero;
                Quaternion r = Quaternion.Euler(0, 0, 0);
                go.transform.localRotation = r;
                _sword.Add(twinSword.WeaponName, go);
            }

            _sword[_playerControl.WeaponSetting.NowWeapon.WeaponName].SetActive(false);
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

                    _playerControl.Rb.velocity = Vector3.zero;
                    _isAttack = true;

                    //������o��
                    _playerControl.WeaponSetting.WeaponActives(true);

                    //�p�[�e�B�N�����~�߂�
                    _playerControl.ParticleControl.WarpParticle(false);

                    _playerControl.AnimControl.AssistSet(false);
                    _playerControl.Animator.SetInteger("AttackType", 3);
                    _playerControl.AnimControl.AttackAnim();
                }
            }
        }
    }

    public override void ShiftBreakAttackEnd()
    {
        if (_playerControl.WeaponSetting.NowWeapon is ScritableWeaponTwinSword twinSword)
        {
            if (_sword.ContainsKey(twinSword.WeaponName))
            {
                _sword[twinSword.WeaponName].SetActive(true);
            }   //������̕����\��

        }
    }

    public override void StopAttackForDamage()
    {
        _isAttack = false;
    }
}

