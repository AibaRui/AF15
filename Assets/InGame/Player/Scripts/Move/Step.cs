using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Step
{
    [Header("�X�e�b�v�̈ړ����x")]
    [SerializeField] private float _stepSpeed = 7f;

    [Header("�X�e�b�v�̋���")]
    [SerializeField] private float _stepDistance = 4f;

    [Header("�X�e�b�v�̃A�j���[�V������")]
    [SerializeField] private string _animName = "Step_1";

    [SerializeField] private string _layerAvoid;

    [SerializeField] private string _layerPlayer;

    private PlayerControl _playerControl;

    private Vector3 _dir = Vector3.zero;


    private int _stepCount = 0;

    private bool _isStepNow = false;

    private bool _isCanStep = true;

    private Vector3 _startStepPos;


    public bool IsStepNow => _isStepNow;
    public bool IsCanStep { get => _isCanStep; set => _isCanStep = value; }

    public int StepCount => _stepCount;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    public void ResetStep()
    {
        _isCanStep = true;
        _stepCount = 0;
    }

    public void StepStartSetting()
    {
        _playerControl.AnimControl.StepSet(true);
        _playerControl.AnimControl.PlayerAnimPlay(_animName);

        _isStepNow = true;

        //�X�e�b�v�̉񐔑���
        _stepCount++;

        if (_playerControl._stepInfo.StepLevel <= _stepCount)
        {
            _isCanStep = false;
        }    //�񐔐����܂ōs������s�ɂ���

        //�X�e�b�v�J�n�n�_��ۑ�
        _startStepPos = _playerControl.PlayerT.position;

        //���G���C���[�ɕύX
        _playerControl.LayerControl.SetPlayerGodLayer();

        //�X�e�b�v�̕����ݒ�
        StepDirSetting();

        //�c���ݒ�
        _playerControl.AfterImage.PlayAfterImage(_animName, _startStepPos, _playerControl.PlayerT.rotation);

        //�p�[�e�B�N���𔭐�
        _playerControl.ParticleControl.WarpParticle(true);
    }

    public bool CheckMoveDistance()
    {
        if (Vector3.Distance(_playerControl.PlayerT.position, _startStepPos) >= _stepDistance)
        {
            _isStepNow = false;

            //�p�[�e�B�N��������
            _playerControl.ParticleControl.WarpParticle(false);

            //�ʏ탌�C���[�ɕύX
            _playerControl.LayerControl.SetPlayerDefaltLayer();

            _playerControl.Rb.velocity = Vector3.zero;
            _playerControl.AnimControl.StepSet(false);
            return true;
        }
        return false;
    }

    public void StepFixedUpdata()
    {
        _playerControl.Rb.velocity = _dir * _stepSpeed;

        if (_playerControl.EnemyCheck.NowEnemy != null)
        {

            //��]
            Vector3 dir = (_playerControl.EnemyCheck.NowEnemy.transform.position - _playerControl.PlayerT.position).normalized;
            var rotationSpeed = 200 * Time.deltaTime;
            Quaternion _targetRotation = Quaternion.LookRotation(dir, Vector3.up);
            _playerControl.PlayerT.rotation = Quaternion.RotateTowards(_playerControl.PlayerT.rotation, _targetRotation, rotationSpeed);
        }
    }

    public void StepDirSetting()
    {

        float h = _playerControl.InputManager.HorizontalInput;
        float v = _playerControl.InputManager.VerticalInput;

        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        Vector3 moveDir = default;

        if (h != 0 || v != 0)
        {
            moveDir = horizontalRotation * new Vector3(h, 0.3f, v).normalized;
        }
        else
        {
            if (_playerControl.EnemyCheck.NowEnemy != null)
            {
                moveDir = -(_playerControl.EnemyCheck.NowEnemy.transform.position - _playerControl.PlayerT.position).normalized;
            }
            else
            {
                moveDir = -_playerControl.PlayerT.forward;
            }
            moveDir.y = 0.3f;
        }



        _dir = moveDir;
    }

}
