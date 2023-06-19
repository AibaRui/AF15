using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShiftBreak : IPlayerAction
{
    [Header("����̃X�^�[�g�ʒu")]
    [SerializeField] private Transform _weaponSetPos;

    [Header("�N�[���^�C��")]
    [SerializeField] private float _coolTime = 2;

    [Header("�V�t�g�u���C�N�I�����n��ŉ����鑬�x")]
    [SerializeField] private float _endAddSpeedOnGround = 15;

    [Header("�V�t�g�u���C�N�I�����󒆂ŉ����鑬�x")]
    [SerializeField] private float _endAddSpeedOnAir = 3;

    [Header("�^�[�Q�b�g����̎��̍ő勗��")]
    [SerializeField] private float _maxMoveDistanceOnTarget = 20;

    [Header("�^�[�Q�b�g����̎��A�Œ�ǂ̂��炢����Ă����烏�[�v�ł��邩")]
    [SerializeField] private float _minDistanceWarpIsTargetting = 3;

    [Header("�G�̃��C���[")]
    [SerializeField] private LayerMask _enemyLayer;

    [Header("��Q���Ƃ݂Ȃ����ׂẴ��C���[")]
    [SerializeField] private LayerMask _wallsLayer;

    [Header("�V�t�g�u���C�N�I���̃A�j���[�V������")]
    [SerializeField] private string _shiftBrakEndName;

    /// <summary>���ݓ����Ă��镐��̃I�u�W�F�N�g</summary>
    private GameObject _nowThrowWeapon;

    /// <summary>���ݓ����Ă��镐���RigidBody</summary>
    private Rigidbody _weaponRb;

    /// <summary>�����镐��̃I�u�W�F�N�g��o�^���Ă��鎫��</summary>
    Dictionary<string, GameObject> _weapon = new Dictionary<string, GameObject>();

    //�V�t�g�u���C�N���G�ɓ����������ǂ���
    private bool _isHitEnemy = false;

    /// <summary>�V�t�g�u���C�N�����ǂ���</summary>
    private bool _isShiftBreakNow = false;

    /// <summary>�V�t�g�u���C�N�J�n�n�_</summary>
    private Vector3 _startPos;

    /// <summary>�V�t�g�u���C�N�̓��B�n�_</summary>
    private Vector3 _targetPos = default;

    /// <summary>�ړ�����</summary>
    Vector3 _moveDir = default;

    /// <summary>�G�t�F�N�g���o�������ǂ���</summary>
    private bool _isUseEffeck = false;

    /// <summary>�G�Ɍ����ăV�t�g�u���C�N���������ǂ���</summary>
    private bool _isTarget;

    /// <summary>�V�t�g�u���C�N�i�s�����̏�Q���̌��m�p</summary>
    private RaycastHit _hit;

    /// <summary>�G�Ɍ����ăV�t�g�u���C�N������A�U�����I�������ǂ���</summary>
    private bool _isAttack = false;

    /// <summary>�N�[���^�C���v���p</summary>
    private float _countCoolTime = 0;

    /// <summary>�V�t�g�u���C�N�\���ǂ���</summary>
    private bool _isCanShiftBreak = true;


    private ShiftBreakSetting _shiftBreakSetting;

    public bool IsCanShiftBreak => _isCanShiftBreak;

    public bool IsAttack => _isAttack;

    public bool IsTartget => _isTarget;
    public GameObject NowThrowWeapon => _nowThrowWeapon;

    public bool IsHitEnemy => _isHitEnemy;

    public bool IsShiftBreake { get => _isShiftBreakNow; set => _isShiftBreakNow = value; }




    /// <summar>�V�t�g�u���C�N�J�n���ɌĂ�</summary>
    public void StartShiftBreak()
    {
        _playerControl.SoundManager.PlaySound(AudioType.Shift);

        _shiftBreakSetting = _playerControl.AllWeapons.GetWeaponData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ShiftBreakSetting;

        _playerControl.WeaponSetting.CheckWeapon();

        //���݃V�t�g�u���C�N��
        _isShiftBreakNow = true;

        _isTarget = _playerControl.EnemyCheck.IsTargetting;

        _isAttack = false;

        _isUseEffeck = false;

        //�d�͂��I�t
        _playerControl.Rb.useGravity = false;

        //�����l�̐ݒ�
        _startPos = _playerControl.PlayerT.position;

        //���x��0�ɂ���
        _playerControl.Rb.velocity = Vector3.zero;

        //�A�j���[�V�����̍Đ�
        _playerControl.AnimControl.PlayerAnimPlay(_shiftBreakSetting.ShiftBreakAnimName);

        //�V�t�g�u���C�N�̌����̐ݒ�
        DirectionSetting();

        if (_isTarget)
        {
            _playerControl.PlayerT.forward = _moveDir;
        }

        //����̐ݒ�(�\��)
        WeaponSet();
    }


    /// <summary>�V�t�g�u���C�N�̌����̐ݒ�</summary>
    public void DirectionSetting()
    {
        RaycastHit hit;
        if (_playerControl.EnemyCheck.IsTargetting && _playerControl.EnemyCheck.NowEnemy!=null)
        {
            _moveDir = (_playerControl.EnemyCheck.NowEnemy.transform.position - _playerControl.PlayerT.position).normalized;
        }
        else
        {
            _moveDir = _playerControl.PlayerT.forward;
        }

        bool isHit = Physics.Raycast(_playerControl.PlayerT.position, _moveDir, out hit, _maxMoveDistanceOnTarget, _wallsLayer);

        if (isHit)
        {
            Vector3 moveDir = (hit.point - _playerControl.PlayerT.position).normalized;
            float distance = (Vector3.Distance(hit.point, _playerControl.PlayerT.position)) * 0.9f;

            _targetPos = _playerControl.PlayerT.position + moveDir * distance;
        }   //�����ɓ��������ꍇ���̎�O�܂ňړ�����
        else
        {
            _targetPos = _playerControl.PlayerT.position + _playerControl.PlayerT.forward * _shiftBreakSetting.MaxMoveDistance;
        }   //���ɂ�������Ȃ�������ő勗���܂Ői��

    }



    /// <summary>�V�t�g�u���C�N���̕���̐ݒ�</summary>
    public void WeaponSet()
    {
        //���킪���ɓo�^�ς݂̕���Ȃ�ė��p
        if (_weapon.ContainsKey(_playerControl.WeaponSetting.NowWeapon.name))
        {
            _nowThrowWeapon = _weapon[_playerControl.WeaponSetting.NowWeapon.name];
        }
        else    //���킪�o�^�ς݂Ŗ��������琶�����Ēǉ�
        {
            //����𐶐���RigidBody�����āA�����ɓo�^
            var weaponObject = _playerControl.InstantiateObject(_playerControl.WeaponSetting.NowWeapon.WeaponLookObject);
            weaponObject.AddComponent<Rigidbody>();
            weaponObject.GetComponent<Rigidbody>().useGravity = false;
            _weapon.Add(_playerControl.WeaponSetting.NowWeapon.name, weaponObject);

            //���g����������܍�������̂ɂɐݒ�
            _nowThrowWeapon = weaponObject;
        }

        _weaponRb = _nowThrowWeapon.GetComponent<Rigidbody>();

        //�ڕW�n�_�������悤�ɂ���
        _nowThrowWeapon.transform.up = (_targetPos - _playerControl.PlayerT.position).normalized;

        //����̈ʒu�𓊝��ʒu�ɐݒ�
        _nowThrowWeapon.transform.localPosition = _weaponSetPos.position;

        //��U��\��
        _nowThrowWeapon.SetActive(false);
    }



    /// <summary>�V�t�g�u���C�N���̓���</summary>
    public void MoveShiftBreak()
    {
        if (_isAttack) return;

        if (_nowThrowWeapon.activeSelf)
        {
            //����������̈ړ�����
            WeaponMove();

            //��苗���܂ł����񂾂�c�����o���n�߂�
            if (Vector3.Distance(_targetPos, _nowThrowWeapon.transform.position) < 4 && !_isUseEffeck)
            {
                AfterImageStarting();
            }

            //��苗���܂Œm��������I��
            if (Vector3.Distance(_targetPos, _nowThrowWeapon.transform.position) < 2)
            {
                _playerControl.SoundManager.PlaySound(AudioType.ShiftEnd);
                MoveEndPlayerPosSet();

                //�p�[�e�B�N��
                _playerControl.ParticleControl.ShiftBreakParticle(true);

                //����̑��x��0�ɂ���
                _weaponRb.velocity = Vector3.zero;

                //������������\��
                _nowThrowWeapon.SetActive(false);

                EndAttack();

                //���ɖ߂�
                _playerControl.MaterialControl.ChangeMaterial(MaterialControl.PlayerMaterialType.Nomal);
            }
        }
    }

    /// <summary>
    /// �ړ�������ɍU�����邩���Ȃ����̏���
    /// </summary>
    public void EndAttack()
    {
        if (_isTarget)
        {
            ShiftBreakAttack();

        }   //�G�Ɍ����ăV�t�g�u���C�N���Ă����ꍇ
        else
        {
            //�d�͂��I��
            _playerControl.Rb.useGravity = true;

            //�V�t�g�u���C�N�I����̃v���C���[�̍Đ�
            _playerControl.AnimControl.PlayerAnimPlay(_shiftBrakEndName);
        }   //�G�Ɍ����ăV�t�g�u���C�N���Ă��Ȃ������ꍇ
    }

    /// <summary>�V�t�g�u���C�N�̍U��</summary>
    public void ShiftBreakAttack()
    {
        if (_playerControl.WeaponSetting.NowWeapon.WeaponType == AllWeapons.WeaponType.Gun)
        {
            _playerControl.SoundManager.PlaySound(AudioType.Fire);
        }
        else
        {
            _playerControl.SoundManager.PlaySound(AudioType.Attack);
        }

        _isAttack = true;

        //�U������
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ShiftBreakAttack();

        //�����\��
        _playerControl.WeaponSetting.WeaponActives(true);

        //�U���A�j���[�V����
        _playerControl.AnimControl.PlayerAnimPlay(_shiftBreakSetting.ShiftBreakAttackAnimName);
    }

    /// <summary>�i�s�����ɏ�Q�����Ȃ��������m����</summary>
    /// <returns>��Q�������������ǂ���</returns>
    public bool CheckMoveDir()
    {
        return Physics.Linecast(_playerControl.PlayerT.position, _nowThrowWeapon.transform.position, out _hit, _wallsLayer);
    }


    /// <summary>�v���C���[�̃��[�v����</summary>
    public void MoveEndPlayerPosSet()
    {
        if (CheckMoveDir())
        {
            _targetPos = _hit.point + (-_moveDir * 1);
        }   //�i�s�����ɏ�Q�������邩�ǂ������m�F

        if (_isTarget)
        {
            if (_playerControl.EnemyCheck.NowEnemy != null)
            {
                //�G�Ƃ̋������ׁA��苗������Ă����烏�[�v����
                RaycastHit hit;
                Vector3 dirs = _playerControl.EnemyCheck.NowEnemy.transform.position - _playerControl.PlayerMidlePos.position;
                Physics.Raycast(_playerControl.PlayerMidlePos.position, dirs.normalized, out hit, 20, _enemyLayer);
                float dis = Vector3.Distance(_playerControl.PlayerMidlePos.position, hit.point);

                if (dis >= _minDistanceWarpIsTargetting)
                {
                    //�v���C���[�����[�v��̈ʒu�Ɉړ�
                    _playerControl.PlayerT.position = _targetPos;
                }
            }
            else
            {
                //�v���C���[�����[�v��̈ʒu�Ɉړ�
                _playerControl.PlayerT.position = _targetPos;
            }
        }
        else
        {
            //�v���C���[�����[�v��̈ʒu�Ɉړ�
            _playerControl.PlayerT.position = _targetPos;
        }

        if (_isTarget) return;

        //���������
        Vector3 dir = _targetPos - _playerControl.PlayerT.position;
        //�������
        float speed = _playerControl.GroundCheck.IsHit() ? _endAddSpeedOnGround : _endAddSpeedOnAir;

        //�v���C���[�𐳖ʂɏ����ړ�������
        _playerControl.Rb.velocity = _moveDir * speed;
    }


    /// <summary>������������̈ړ�����</summary>
    public void WeaponMove()
    {
        //���������
        Vector3 dir = _targetPos - _playerControl.PlayerT.position;

        //����ɑ��x��������
        _weaponRb.velocity = dir.normalized * _shiftBreakSetting.MoveSpeed;
    }

    /// <summary>�c�����́B���[�v���̉��o�̐ݒ�</summary>
    public void AfterImageStarting()
    {
        _isUseEffeck = true;

        //�c�����o��
        _playerControl.AfterImage.PlayAfterImage(_shiftBreakSetting.ShiftBreakAnimName, _startPos, _playerControl.PlayerT.rotation);

        //�����ɂ���
        _playerControl.MaterialControl.ChangeMaterial(MaterialControl.PlayerMaterialType.Clear);
    }

    /// <summary>�V�t�g�u���C�N�I�����̐ݒ�</summary>
    public void EndShifrBreak()
    {

        //�p�[�e�B�N��
        _playerControl.ParticleControl.ShiftBreakParticle(false);

        _isCanShiftBreak = false;
    }

    /// <summary>�V�t�g�u���C�N�U���I�����̐ݒ�</summary>
    public void ShiftBreakAttackEnd()
    {
        //�d�͂��I��
        _playerControl.Rb.useGravity = true;

        _isShiftBreakNow = false;

        _isAttack = false;

        //������������\��
        _playerControl.WeaponSetting.WeaponActives(false);
    }

    /// <summary>�V�t�g�u���C�N�̃N�[���^�C���v��</summary>
    public void CountCoolTime()
    {
        if (!_isCanShiftBreak)
        {
            _countCoolTime += Time.deltaTime;

            if (_countCoolTime >= _coolTime)
            {
                _isCanShiftBreak = true;
                _countCoolTime = 0;
            }
        }
    }
}
