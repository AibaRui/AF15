using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Avoid
{
    [SerializeField] private Vector3 _speedLimit = new Vector3(20, 20, 20);

    [Header("ロール回避のアニメーション名")]
    [SerializeField] private string _avoidRoalAnimName;

    [Header("地面での回避のアニメーション名")]
    [SerializeField] private string _avoidGroundAnimName;

    [Header("空中での回避のアニメーション名")]
    [SerializeField] private string _avoidAirAnimName;


    [Header("回避の速度")]
    [SerializeField] private float _avoidSpeed = 5;

    [Header("回避のクール時間")]
    [SerializeField] private float _coolTime = 2;

    [Header("回避の実行時間")]
    [SerializeField] private float _avoidTime = 2;

    [Header("ロール回避の速度")]
    [SerializeField] private float _roalAvoidSpeed = 5;

    [Header("ロール回避のクール時間")]
    [SerializeField] private float _roalCoolTime = 2;

    [Header("ロール回避の実行時間")]
    [SerializeField] private float _roalAvoidTime = 2;

    [Header("ロール回避の実行のINput時間")]
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

    /// <summary>回避を構えて入り最中かどうかを調べる </summary>
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

        //無敵レイヤーに変更
        _playerControl.LayerControl.SetPlayerGodLayer();

        _playerControl.HoldAvoidMove.IsDamage = false;
        _playerControl.AnimControl.AvoidSet(true);
    }

    public void SetSpeedLimit()
    {
        _playerControl.VelocityLimit.SetLimit(_speedLimit.x, _speedLimit.y, _speedLimit.z);
    }

    /// <summary>回避のクールタイムを数える</summary>
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
        //移動入力を受け取る
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


    /// <summary>回避の速度設定</summary>
    public void DoAvoid()
    {
        //回避が終わっていない間回す
        if (_isEndAvoid)
        {
            return;
        }

        float speed = _isRoal ? _roalAvoidSpeed : _avoidSpeed;

        //速度を加える
        _playerControl.Rb.velocity = _avoidDir * speed;

        //回避時間を計測
        _countAvoidTime += Time.deltaTime;


        float setLimit = _isRoal ? _roalAvoidTime : _avoidTime;

        //回避時間が経過、もしくは回避終了で終わり
        if (_countAvoidTime >= setLimit || _isEndAvoid)
        {
            _playerControl.AnimControl.AvoidSet(false);
            _isEndAvoid = true;
            return;
        }
    }


    /// <summary>回避終了時の処理</summary>
    public void EndAvoid()
    {
        //無敵レイヤーに変更
        _playerControl.LayerControl.SetPlayerDefaltLayer();

        //重力をオン
        _playerControl.Rb.useGravity = true;

        _isRoal = false;

        //回避可能フラグ
        _isCanAvoid = false;

        //回避時間計測のタイムをリセット
        _countAvoidTime = 0;

        //少し前にすべるように
        _playerControl.Rb.velocity = _avoidDir * _roalAvoidSpeed / 5;
    }

    public void SetAvoidRoal()
    {
        _isRoal = true;
    }


}
