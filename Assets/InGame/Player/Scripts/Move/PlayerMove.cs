using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove : IPlayerAction
{
    [Header("歩く速度")]
    [SerializeField] private float _walkSpeed = 4;

    [Header("走る速度")]
    [SerializeField] private float _runSpeed = 4;

    [Header("空中での速度")]
    [SerializeField] private float _airMoveSpeed = 4;

    [Header("ジャンプパワー")]
    [SerializeField] private float _jumpPower = 4;

    [Header("歩きの時の回転速度")]
    [SerializeField] private float _walkRotateSpeed = 100;

    [Header("走りの時の回転速度")]
    [SerializeField] private float _runRotateSpeed = 100;

    [Header("重力")]
    [SerializeField] private float _gravity = 0.9f;

    /// <summary>入力方向</summary>
    private Vector3 velo;

    /// <summary>向くべきプレイヤーの回転</summary>
    Quaternion _targetRotation;


    public enum MoveType
    {
        Walk,
        Run,
    }

    public void ReSetTime()
    {

    }

    public void Move(MoveType moveType)
    {

        //移動方向の転換速度
        float turnSpeed = 0;

        //移動速度
        float moveSpeed = 0;

        //走り方によって速度を変更
        if (moveType == MoveType.Walk)
        {
            turnSpeed = _walkRotateSpeed;
            moveSpeed = _walkSpeed;
        }
        else
        {
            turnSpeed = _runRotateSpeed;
            moveSpeed = _runSpeed;
        }

        //移動入力を受け取る
        float h = _playerControl.InputManager.HorizontalInput;
        float v = _playerControl.InputManager.VerticalInput;



        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        velo = horizontalRotation * new Vector3(h, 0, v).normalized;
        var rotationSpeed = turnSpeed * Time.deltaTime;

        if (velo.magnitude > 0.5f)
        {
            _targetRotation = Quaternion.LookRotation(velo, Vector3.up);

        }

        //if (_targetRotation.eulerAngles.y > 180)
        //{
        //    var angle = _targetRotation.eulerAngles.y - 360;
        //    Quaternion rotation = Quaternion.Euler(angle * Vector3.up);
        //    _targetRotation = rotation;
        //    //    Debug.Log("DAA" + _targetRotation.eulerAngles.y);
        //}

        _playerControl.PlayerT.rotation = Quaternion.RotateTowards(_playerControl.PlayerT.rotation, _targetRotation, rotationSpeed);


        //速度を加える
        _playerControl.Rb.velocity = velo * moveSpeed;
        //重力を加える
        //_playerControl.Rb.AddForce(Vector3.down * _gravity);
    }


    /// <summary>空中での動き</summary>
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

    public void Jump()
    {
        _playerControl.SoundManager.PlaySound(AudioType.Jump);
        Vector3 velo = new Vector3(_playerControl.Rb.velocity.x, _jumpPower, _playerControl.Rb.velocity.z);
        _playerControl.Rb.velocity = velo;
    }


}
