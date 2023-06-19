using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoldAvoid : IPlayerAction
{
    [Header("�������x")]
    [SerializeField] private float _walkSpeed = 4;

    [Header("�󒆂ł̑��x")]
    [SerializeField] private float _airMoveSpeed = 4;

    [Header("�����̎��̉�]���x")]
    [SerializeField] private float _walkRotateSpeed = 100;

    [Header("�d��")]
    [SerializeField] private float _gravity = 0.9f;

    /// <summary>���͕���</summary>
    private Vector3 velo;

    private bool _isHoldAvoid = false;

    private bool _isDamage;

    /// <summary>�����ׂ��v���C���[�̉�]</summary>
    Quaternion _targetRotation;

    public bool IsHoldAvoid { get => _isHoldAvoid; set => _isHoldAvoid = value; }

    public bool IsDamage { get => _isDamage; set => _isDamage = value; }

    public void ReSetTime()
    {

    }

    public void Move()
    {

        //�ړ������̓]�����x
        float turnSpeed = 0;

        //�ړ����x
        float moveSpeed = 0;

        turnSpeed = _walkRotateSpeed;
        moveSpeed = _walkSpeed;

        //�ړ����͂��󂯎��
        float h = _playerControl.InputManager.HorizontalInput;
        float v = _playerControl.InputManager.VerticalInput;

        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        velo = horizontalRotation * new Vector3(h, 0, v).normalized;
        var rotationSpeed = turnSpeed * Time.deltaTime;

        if (velo.magnitude > 0.5f)
        {
            _targetRotation = Quaternion.LookRotation(velo, Vector3.up);

        }

        _playerControl.PlayerT.rotation = Quaternion.RotateTowards(_playerControl.PlayerT.rotation, _targetRotation, rotationSpeed);


        //���x��������
        _playerControl.Rb.velocity = velo * moveSpeed;
    }


    /// <summary>�󒆂ł̓���</summary>
    public void AirMove()
    {
        float h = _playerControl.InputManager.HorizontalInput;
        float v = _playerControl.InputManager.VerticalInput;

        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        velo = horizontalRotation * new Vector3(h, 0, v).normalized;
        var rotationSpeed = 100 * Time.deltaTime;

        if (velo.magnitude > 0.5f)
        {
            _targetRotation = Quaternion.LookRotation(velo, Vector3.up);
        }

        _playerControl.PlayerT.rotation = Quaternion.RotateTowards(_playerControl.PlayerT.rotation, _targetRotation, rotationSpeed);


        float speed = 0;

        speed = _airMoveSpeed;

        _playerControl.Rb.AddForce(velo * speed);
    }

}
