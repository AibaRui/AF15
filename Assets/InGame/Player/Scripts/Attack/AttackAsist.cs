using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackAsist
{
    [Header("プレイヤーの回転補正速度")]
    [SerializeField] private float _playerRotateSpeed = 400;

    [Header("アニメーション名")]
    [SerializeField] private string _animName = "AssistAirMoe";

    [Header("敵に近づいたとする最低距離")]
    [SerializeField] private float _approachMinDistance = 2;

    [Header("敵のレイヤー")]
    [SerializeField] private LayerMask _enemyLayer;

    /// <summary>回転が終了したかどうか</summary>
    private bool _rotateEnd = false;

    /// <summary>アシストの移動が終了したかどうか</summary>
    private bool _isMoveEnd = false;

    /// <summary>ターゲットしている敵</summary>
    private GameObject _target;

    /// <summary>ターゲットしている敵へのRayの方向</summary>
    private Vector3 _rayDir = Vector3.zero;

    /// <summary>ターゲットしている敵の方向</summary>
    private Vector3 _dirOfTarget = Vector3.zero;

    /// <summary>ターゲットしている敵への回転方向</summary>
    private Quaternion _targetRotation;

    /// <summary>プレイヤーの回転速度</summary>
    private float _rotationSpeed;

    private PlayerControl _playerControl;

    public bool RotateEnd => _rotateEnd;
    public string AnimName => _animName;
    public Quaternion TargetRotatiom => _targetRotation;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>アシストの初期設定</summary>
    /// <param name="target">ターゲットしている敵</param>
    public void AssistSet(GameObject target)
    {
        //ターゲットと、アシスト移動の向きを設定
        if (target != null)
        {
            _target = target;

            _dirOfTarget = (target.transform.position - _playerControl.PlayerT.position).normalized;
        }   //ターゲットが入る場合
        else
        {
            _target = null;
            _dirOfTarget = _playerControl.PlayerT.forward;
        }   //ターゲットがいないばあい

        _rayDir = _dirOfTarget;

        //プレイヤーの回転方向を設定
        SetRotation();

        //敵との距離を確認して、移動が必要かどうかを判断
        _isMoveEnd = CheckApproachDistance();
    }

    /// <summary>アシストの、移動処理</summary>
    /// <param name="speed">移動速度</param>
    public void AttaclkAssistMove(float speed)
    {
        Vector3 moveDir = default;

        if (_playerControl.GroundCheck.IsHit())
        {
            moveDir = new Vector3(_dirOfTarget.x, 0, _dirOfTarget.z);
        }   //地面での場合
        else
        {
            moveDir = _dirOfTarget;
        }   //空中での場合

        //速度を設定
        _playerControl.Rb.velocity = moveDir * speed;
    }

    /// <summary>プレイヤーの回転方向を計算</summary>
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

        //回転速度の設定
        _rotationSpeed = _playerRotateSpeed * Time.deltaTime;

        //回転方向
        _targetRotation = Quaternion.LookRotation(rotateDir, Vector3.up);
    }


    /// <summary>アシストの、回転処理</summary>
    public void AttackAssistRotation()
    {
        //回転方向を設定
        SetRotation();

        //プレイヤーの回転を設定
        _playerControl.PlayerT.rotation = Quaternion.RotateTowards(_playerControl.PlayerT.rotation, _targetRotation, _rotationSpeed);
    }

    /// <summary>ターゲットとの距離が近いかどうかを確認</summary>
    /// <returns>ターゲットに十分近づいたかどうか</returns>
    public bool CheckApproachDistance()
    {
        //nullチェック
        if (_target == null) return false;

        RaycastHit hit;

        Physics.Raycast(_playerControl.PlayerT.position, _rayDir, out hit, _enemyLayer);

        //敵に近づいた
        if (Vector3.Distance(hit.point, _playerControl.PlayerT.position) < _approachMinDistance)
        {
            return true;
        }
        return false;
    }


    /// <summary>空中でのアシスト</summary>
    /// <param name="startPos">アシストの開始地点</param>
    /// <param name="maxMoveDistance">アシストの最大距離</param>
    /// <returns>移動が完了したかどうか</returns>
    public bool AttackAssistAir(Vector3 startPos, float maxMoveDistance)
    {
        if (_target != null)
        {
            //敵の方向
            _dirOfTarget = (_target.transform.position - _playerControl.PlayerT.position).normalized;
            _rayDir = _dirOfTarget;
        }
        else
        {
            _dirOfTarget = _playerControl.PlayerT.forward;
            _rayDir = _dirOfTarget;
        }

        //敵に近づいた
        if (CheckApproachDistance())
        {
            _isMoveEnd = true;
            return true;
        }

        //最長距離より超過
        if (Vector3.Distance(startPos, _playerControl.PlayerT.position) > maxMoveDistance)
        {
            return true;
        }
        return false;
    }


    /// <summary>地上でのアシスト</summary>
    /// <param name="startPos">アシストの開始地点</param>
    /// <param name="maxMoveDistance">アシストの最大距離</param>
    /// <returns>ターゲットに十分近づいたかどうか</returns>
    public bool AttackAssistGround(Vector3 startPos, float maxMoveDistance)
    {
        if (_target != null)
        {
            //敵の方向
            _dirOfTarget = (_target.transform.position - _playerControl.PlayerT.position).normalized;
            _rayDir = _dirOfTarget;
            _dirOfTarget.y = 0;
        }
        else
        {
            _dirOfTarget = _playerControl.PlayerT.forward;
            _rayDir = _dirOfTarget;
        }

        //敵に近づいた
        if (CheckApproachDistance())
        {
            _isMoveEnd = true;
            return true;
        }
        //最長距離より超過
        else if (Vector3.Distance(startPos, _playerControl.PlayerT.position) > maxMoveDistance)
        {
            return true;
        }
        return false;
    }

}
