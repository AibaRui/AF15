using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackAsist
{
    [Header("�v���C���[�̉�]�␳���x")]
    [SerializeField] private float _playerRotateSpeed = 400;

    [Header("�A�j���[�V������")]
    [SerializeField] private string _animName = "AssistAirMoe";

    [Header("�G�ɋ߂Â����Ƃ���Œ዗��")]
    [SerializeField] private float _approachMinDistance = 2;

    [Header("�G�̃��C���[")]
    [SerializeField] private LayerMask _enemyLayer;

    /// <summary>��]���I���������ǂ���</summary>
    private bool _rotateEnd = false;

    /// <summary>�A�V�X�g�̈ړ����I���������ǂ���</summary>
    private bool _isMoveEnd = false;

    /// <summary>�^�[�Q�b�g���Ă���G</summary>
    private GameObject _target;

    /// <summary>�^�[�Q�b�g���Ă���G�ւ�Ray�̕���</summary>
    private Vector3 _rayDir = Vector3.zero;

    /// <summary>�^�[�Q�b�g���Ă���G�̕���</summary>
    private Vector3 _dirOfTarget = Vector3.zero;

    /// <summary>�^�[�Q�b�g���Ă���G�ւ̉�]����</summary>
    private Quaternion _targetRotation;

    /// <summary>�v���C���[�̉�]���x</summary>
    private float _rotationSpeed;

    private PlayerControl _playerControl;

    public bool RotateEnd => _rotateEnd;
    public string AnimName => _animName;
    public Quaternion TargetRotatiom => _targetRotation;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>�A�V�X�g�̏����ݒ�</summary>
    /// <param name="target">�^�[�Q�b�g���Ă���G</param>
    public void AssistSet(GameObject target)
    {
        //�^�[�Q�b�g�ƁA�A�V�X�g�ړ��̌�����ݒ�
        if (target != null)
        {
            _target = target;

            _dirOfTarget = (target.transform.position - _playerControl.PlayerT.position).normalized;
        }   //�^�[�Q�b�g������ꍇ
        else
        {
            _target = null;
            _dirOfTarget = _playerControl.PlayerT.forward;
        }   //�^�[�Q�b�g�����Ȃ��΂���

        _rayDir = _dirOfTarget;

        //�v���C���[�̉�]������ݒ�
        SetRotation();

        //�G�Ƃ̋������m�F���āA�ړ����K�v���ǂ����𔻒f
        _isMoveEnd = CheckApproachDistance();
    }

    /// <summary>�A�V�X�g�́A�ړ�����</summary>
    /// <param name="speed">�ړ����x</param>
    public void AttaclkAssistMove(float speed)
    {
        Vector3 moveDir = default;

        if (_playerControl.GroundCheck.IsHit())
        {
            moveDir = new Vector3(_dirOfTarget.x, 0, _dirOfTarget.z);
        }   //�n�ʂł̏ꍇ
        else
        {
            moveDir = _dirOfTarget;
        }   //�󒆂ł̏ꍇ

        //���x��ݒ�
        _playerControl.Rb.velocity = moveDir * speed;
    }

    /// <summary>�v���C���[�̉�]�������v�Z</summary>
    public void SetRotation()
    {
        Vector3 rotateDir = default;

        if (_playerControl.GroundCheck.IsHit())
        {
            rotateDir = new Vector3(_dirOfTarget.x, 0, _dirOfTarget.z);
        }
        else
        {
            rotateDir = _dirOfTarget;
        }

        //��]���x�̐ݒ�
        _rotationSpeed = _playerRotateSpeed * Time.deltaTime;

        //��]����
        _targetRotation = Quaternion.LookRotation(rotateDir, Vector3.up);
    }


    /// <summary>�A�V�X�g�́A��]����</summary>
    public void AttackAssistRotation()
    {
        //��]������ݒ�
        SetRotation();

        //�v���C���[�̉�]��ݒ�
        _playerControl.PlayerT.rotation = Quaternion.RotateTowards(_playerControl.PlayerT.rotation, _targetRotation, _rotationSpeed);
    }

    /// <summary>�^�[�Q�b�g�Ƃ̋������߂����ǂ������m�F</summary>
    /// <returns>�^�[�Q�b�g�ɏ\���߂Â������ǂ���</returns>
    public bool CheckApproachDistance()
    {
        //null�`�F�b�N
        if (_target == null) return false;

        RaycastHit hit;

        Physics.Raycast(_playerControl.PlayerT.position, _rayDir, out hit, _enemyLayer);

        //�G�ɋ߂Â���
        if (Vector3.Distance(hit.point, _playerControl.PlayerT.position) < _approachMinDistance)
        {
            return true;
        }
        return false;
    }


    /// <summary>�󒆂ł̃A�V�X�g</summary>
    /// <param name="startPos">�A�V�X�g�̊J�n�n�_</param>
    /// <param name="maxMoveDistance">�A�V�X�g�̍ő勗��</param>
    /// <returns>�ړ��������������ǂ���</returns>
    public bool AttackAssistAir(Vector3 startPos, float maxMoveDistance)
    {
        if (_target != null)
        {
            //�G�̕���
            _dirOfTarget = (_target.transform.position - _playerControl.PlayerT.position).normalized;
            _rayDir = _dirOfTarget;
        }
        else
        {
            _dirOfTarget = _playerControl.PlayerT.forward;
            _rayDir = _dirOfTarget;
        }

        //�G�ɋ߂Â���
        if (CheckApproachDistance())
        {
            _isMoveEnd = true;
            return true;
        }

        //�Œ�������蒴��
        if (Vector3.Distance(startPos, _playerControl.PlayerT.position) > maxMoveDistance)
        {
            return true;
        }
        return false;
    }


    /// <summary>�n��ł̃A�V�X�g</summary>
    /// <param name="startPos">�A�V�X�g�̊J�n�n�_</param>
    /// <param name="maxMoveDistance">�A�V�X�g�̍ő勗��</param>
    /// <returns>�^�[�Q�b�g�ɏ\���߂Â������ǂ���</returns>
    public bool AttackAssistGround(Vector3 startPos, float maxMoveDistance)
    {
        if (_target != null)
        {
            //�G�̕���
            _dirOfTarget = (_target.transform.position - _playerControl.PlayerT.position).normalized;
            _rayDir = _dirOfTarget;
            _dirOfTarget.y = 0;
        }
        else
        {
            _dirOfTarget = _playerControl.PlayerT.forward;
            _rayDir = _dirOfTarget;
        }

        //�G�ɋ߂Â���
        if (CheckApproachDistance())
        {
            _isMoveEnd = true;
            return true;
        }
        //�Œ�������蒴��
        else if (Vector3.Distance(startPos, _playerControl.PlayerT.position) > maxMoveDistance)
        {
            return true;
        }
        return false;
    }

}
