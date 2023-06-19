using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Avoid
{
    [SerializeField] private Vector3 _speedLimit = new Vector3(20, 20, 20);

    [Header("���[������̃A�j���[�V������")]
    [SerializeField] private string _avoidRoalAnimName;

    [Header("�n�ʂł̉���̃A�j���[�V������")]
    [SerializeField] private string _avoidGroundAnimName;

    [Header("�󒆂ł̉���̃A�j���[�V������")]
    [SerializeField] private string _avoidAirAnimName;


    [Header("����̑��x")]
    [SerializeField] private float _avoidSpeed = 5;

    [Header("����̃N�[������")]
    [SerializeField] private float _coolTime = 2;

    [Header("����̎��s����")]
    [SerializeField] private float _avoidTime = 2;

    [Header("���[������̑��x")]
    [SerializeField] private float _roalAvoidSpeed = 5;

    [Header("���[������̃N�[������")]
    [SerializeField] private float _roalCoolTime = 2;

    [Header("���[������̎��s����")]
    [SerializeField] private float _roalAvoidTime = 2;

    [Header("���[������̎��s��INput����")]
    [SerializeField] private float _roalInputTime = 0.3f;

    private float _countInput = 0;

    private float _nowAvoidTime = 0;

    private float _countCoolTime = 0;

    private float _countAvoidTime = 0;

    private bool _isCanAvoid = true;

    private bool _isEndAvoid = false;

    private Vector3 _avoidDir = default;

    private bool _isCanNotAvoid = false;

    private bool _isRoal;

    private bool _isRoalInput = false;

    public bool IsCanNotAvoid { get => _isCanNotAvoid; set => _isCanNotAvoid = value; }


    public bool IsRoalInput => _isRoalInput;

    public bool IsEndAvoid => _isEndAvoid;
    public bool IsCanAvoid => _isCanAvoid;

    private PlayerControl _playerControl;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>������\���ē���Œ����ǂ����𒲂ׂ� </summary>
    public void CheckHoldAvoid()
    {
        _isRoalInput = false;
        if (_playerControl.InputManager.IsInputAvoidButttun)
        {
            _countInput += Time.deltaTime;

            if (_countInput > _roalInputTime)
            {
                _playerControl.HoldAvoidMove.IsHoldAvoid = true;
            }
        }

        if (_playerControl.InputManager.IsInputUpAvoidButttun)
        {
            if (_countInput <= _roalInputTime)
            {
                _isRoalInput = true;
            }

            _countInput = 0;

            _playerControl.HoldAvoidMove.IsHoldAvoid = false;
        }
    }

    public void AvoidStart()
    {
        _playerControl.SoundManager.PlaySound(AudioType.Shift);

        if (_isRoal)
        {
            _playerControl.AnimControl.PlayerAnimPlay(_avoidRoalAnimName);
        }
        else
        {
            string animName = _playerControl.GroundCheck.IsHit() ? _avoidGroundAnimName : _avoidGroundAnimName;

            _playerControl.AfterImage.PlayAfterImage(animName, _playerControl.PlayerT.position, _playerControl.PlayerT.rotation);
        }

        //���G���C���[�ɕύX
        _playerControl.LayerControl.SetPlayerGodLayer();

        _playerControl.HoldAvoidMove.IsDamage = false;
        _playerControl.AnimControl.AvoidSet(true);
    }

    public void SetSpeedLimit()
    {
        _playerControl.VelocityLimit.SetLimit(_speedLimit.x, _speedLimit.y, _speedLimit.z);
    }

    /// <summary>����̃N�[���^�C���𐔂���</summary>
    public void CountCoolTimeAvoid()
    {
        if (_isCanAvoid) return;

        _countCoolTime += Time.deltaTime;

        if (_countCoolTime >= _roalCoolTime)
        {
            _isCanAvoid = true;
            _countCoolTime = 0;
        }
    }

    public void SetAvoidDir()
    {
        //�ړ����͂��󂯎��
        float h = _playerControl.InputManager.HorizontalInput;
        float v = _playerControl.InputManager.VerticalInput;

        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        if (h != 0 || v != 0)
        {
            _avoidDir = horizontalRotation * new Vector3(h, 0, v).normalized;
        }
        else
        {
            _avoidDir = _playerControl.PlayerT.forward;
        }

        Quaternion _targetRotation = Quaternion.LookRotation(_avoidDir, Vector3.up);
        _playerControl.PlayerT.rotation = Quaternion.RotateTowards(_playerControl.PlayerT.rotation, _targetRotation, 360);

        _isEndAvoid = false;

        _nowAvoidTime = _roalAvoidTime;
    }


    /// <summary>����̑��x�ݒ�</summary>
    public void DoAvoid()
    {
        //������I����Ă��Ȃ��ԉ�
        if (_isEndAvoid)
        {
            return;
        }

        float speed = _isRoal ? _roalAvoidSpeed : _avoidSpeed;

        //���x��������
        _playerControl.Rb.velocity = _avoidDir * speed;

        //������Ԃ��v��
        _countAvoidTime += Time.deltaTime;


        float setLimit = _isRoal ? _roalAvoidTime : _avoidTime;

        //������Ԃ��o�߁A�������͉���I���ŏI���
        if (_countAvoidTime >= setLimit || _isEndAvoid)
        {
            _playerControl.AnimControl.AvoidSet(false);
            _isEndAvoid = true;
            return;
        }
    }


    /// <summary>����I�����̏���</summary>
    public void EndAvoid()
    {
        //���G���C���[�ɕύX
        _playerControl.LayerControl.SetPlayerDefaltLayer();

        //�d�͂��I��
        _playerControl.Rb.useGravity = true;

        _isRoal = false;

        //����\�t���O
        _isCanAvoid = false;

        //������Ԍv���̃^�C�������Z�b�g
        _countAvoidTime = 0;

        //�����O�ɂ��ׂ�悤��
        _playerControl.Rb.velocity = _avoidDir * _roalAvoidSpeed / 5;
    }

    public void SetAvoidRoal()
    {
        _isRoal = true;
    }


}
