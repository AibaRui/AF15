using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShiftBreak : IPlayerAction
{
    [Header("武器のスタート位置")]
    [SerializeField] private Transform _weaponSetPos;

    [Header("クールタイム")]
    [SerializeField] private float _coolTime = 2;

    [Header("シフトブレイク終了時地上で加える速度")]
    [SerializeField] private float _endAddSpeedOnGround = 15;

    [Header("シフトブレイク終了時空中で加える速度")]
    [SerializeField] private float _endAddSpeedOnAir = 3;

    [Header("ターゲットありの時の最大距離")]
    [SerializeField] private float _maxMoveDistanceOnTarget = 20;

    [Header("ターゲットありの時、最低どのくらい離れていたらワープできるか")]
    [SerializeField] private float _minDistanceWarpIsTargetting = 3;

    [Header("敵のレイヤー")]
    [SerializeField] private LayerMask _enemyLayer;

    [Header("障害物とみなすすべてのレイヤー")]
    [SerializeField] private LayerMask _wallsLayer;

    [Header("シフトブレイク終わりのアニメーション名")]
    [SerializeField] private string _shiftBrakEndName;

    /// <summary>現在投げている武器のオブジェクト</summary>
    private GameObject _nowThrowWeapon;

    /// <summary>現在投げている武器のRigidBody</summary>
    private Rigidbody _weaponRb;

    /// <summary>投げる武器のオブジェクトを登録している辞書</summary>
    Dictionary<string, GameObject> _weapon = new Dictionary<string, GameObject>();

    //シフトブレイクが敵に当たったかどうか
    private bool _isHitEnemy = false;

    /// <summary>シフトブレイク中かどうか</summary>
    private bool _isShiftBreakNow = false;

    /// <summary>シフトブレイク開始地点</summary>
    private Vector3 _startPos;

    /// <summary>シフトブレイクの到達地点</summary>
    private Vector3 _targetPos = default;

    /// <summary>移動方向</summary>
    Vector3 _moveDir = default;

    /// <summary>エフェクトを出したかどうか</summary>
    private bool _isUseEffeck = false;

    /// <summary>敵に向けてシフトブレイクをしたかどうか</summary>
    private bool _isTarget;

    /// <summary>シフトブレイク進行方向の障害物の検知用</summary>
    private RaycastHit _hit;

    /// <summary>敵に向けてシフトブレイクした後、攻撃し終えたかどうか</summary>
    private bool _isAttack = false;

    /// <summary>クールタイム計測用</summary>
    private float _countCoolTime = 0;

    /// <summary>シフトブレイク可能かどうか</summary>
    private bool _isCanShiftBreak = true;


    private ShiftBreakSetting _shiftBreakSetting;

    public bool IsCanShiftBreak => _isCanShiftBreak;

    public bool IsAttack => _isAttack;

    public bool IsTartget => _isTarget;
    public GameObject NowThrowWeapon => _nowThrowWeapon;

    public bool IsHitEnemy => _isHitEnemy;

    public bool IsShiftBreake { get => _isShiftBreakNow; set => _isShiftBreakNow = value; }




    /// <summar>シフトブレイク開始時に呼ぶ</summary>
    public void StartShiftBreak()
    {
        _playerControl.SoundManager.PlaySound(AudioType.Shift);

        _shiftBreakSetting = _playerControl.AllWeapons.GetWeaponData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ShiftBreakSetting;

        _playerControl.WeaponSetting.CheckWeapon();

        //現在シフトブレイク中
        _isShiftBreakNow = true;

        _isTarget = _playerControl.EnemyCheck.IsTargetting;

        _isAttack = false;

        _isUseEffeck = false;

        //重力をオフ
        _playerControl.Rb.useGravity = false;

        //初期値の設定
        _startPos = _playerControl.PlayerT.position;

        //速度を0にする
        _playerControl.Rb.velocity = Vector3.zero;

        //アニメーションの再生
        _playerControl.AnimControl.PlayerAnimPlay(_shiftBreakSetting.ShiftBreakAnimName);

        //シフトブレイクの向きの設定
        DirectionSetting();

        if (_isTarget)
        {
            _playerControl.PlayerT.forward = _moveDir;
        }

        //武器の設定(表示)
        WeaponSet();
    }


    /// <summary>シフトブレイクの向きの設定</summary>
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
        }   //何かに当たった場合その手前まで移動する
        else
        {
            _targetPos = _playerControl.PlayerT.position + _playerControl.PlayerT.forward * _shiftBreakSetting.MaxMoveDistance;
        }   //何にも当たらなかったら最大距離まで進む

    }



    /// <summary>シフトブレイク時の武器の設定</summary>
    public void WeaponSet()
    {
        //武器が既に登録済みの武器なら再利用
        if (_weapon.ContainsKey(_playerControl.WeaponSetting.NowWeapon.name))
        {
            _nowThrowWeapon = _weapon[_playerControl.WeaponSetting.NowWeapon.name];
        }
        else    //武器が登録済みで無かったら生成して追加
        {
            //武器を生成しRigidBodyをつけて、辞書に登録
            var weaponObject = _playerControl.InstantiateObject(_playerControl.WeaponSetting.NowWeapon.WeaponLookObject);
            weaponObject.AddComponent<Rigidbody>();
            weaponObject.GetComponent<Rigidbody>().useGravity = false;
            _weapon.Add(_playerControl.WeaponSetting.NowWeapon.name, weaponObject);

            //今使う武器をいま作ったものにに設定
            _nowThrowWeapon = weaponObject;
        }

        _weaponRb = _nowThrowWeapon.GetComponent<Rigidbody>();

        //目標地点を向くようにする
        _nowThrowWeapon.transform.up = (_targetPos - _playerControl.PlayerT.position).normalized;

        //武器の位置を投擲位置に設定
        _nowThrowWeapon.transform.localPosition = _weaponSetPos.position;

        //一旦非表示
        _nowThrowWeapon.SetActive(false);
    }



    /// <summary>シフトブレイク中の動き</summary>
    public void MoveShiftBreak()
    {
        if (_isAttack) return;

        if (_nowThrowWeapon.activeSelf)
        {
            //投げた武器の移動処理
            WeaponMove();

            //一定距離まですすんだら残像を出し始める
            if (Vector3.Distance(_targetPos, _nowThrowWeapon.transform.position) < 4 && !_isUseEffeck)
            {
                AfterImageStarting();
            }

            //一定距離まで知数いたら終了
            if (Vector3.Distance(_targetPos, _nowThrowWeapon.transform.position) < 2)
            {
                _playerControl.SoundManager.PlaySound(AudioType.ShiftEnd);
                MoveEndPlayerPosSet();

                //パーティクル
                _playerControl.ParticleControl.ShiftBreakParticle(true);

                //武器の速度を0にする
                _weaponRb.velocity = Vector3.zero;

                //投げた武器を非表示
                _nowThrowWeapon.SetActive(false);

                EndAttack();

                //元に戻す
                _playerControl.MaterialControl.ChangeMaterial(MaterialControl.PlayerMaterialType.Nomal);
            }
        }
    }

    /// <summary>
    /// 移動完了後に攻撃するかしないかの処理
    /// </summary>
    public void EndAttack()
    {
        if (_isTarget)
        {
            ShiftBreakAttack();

        }   //敵に向けてシフトブレイクしていた場合
        else
        {
            //重力をオン
            _playerControl.Rb.useGravity = true;

            //シフトブレイク終了後のプレイヤーの再生
            _playerControl.AnimControl.PlayerAnimPlay(_shiftBrakEndName);
        }   //敵に向けてシフトブレイクしていなかった場合
    }

    /// <summary>シフトブレイクの攻撃</summary>
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

        //攻撃処理
        _playerControl.AllWeapons.WeaponGetActionData(_playerControl.WeaponSetting.NowWeapon.WeaponType).ShiftBreakAttack();

        //武器を表示
        _playerControl.WeaponSetting.WeaponActives(true);

        //攻撃アニメーション
        _playerControl.AnimControl.PlayerAnimPlay(_shiftBreakSetting.ShiftBreakAttackAnimName);
    }

    /// <summary>進行方向に障害物かないかを検知する</summary>
    /// <returns>障害物があったかどうか</returns>
    public bool CheckMoveDir()
    {
        return Physics.Linecast(_playerControl.PlayerT.position, _nowThrowWeapon.transform.position, out _hit, _wallsLayer);
    }


    /// <summary>プレイヤーのワープ処理</summary>
    public void MoveEndPlayerPosSet()
    {
        if (CheckMoveDir())
        {
            _targetPos = _hit.point + (-_moveDir * 1);
        }   //進行方向に障害物があるかどうかを確認

        if (_isTarget)
        {
            if (_playerControl.EnemyCheck.NowEnemy != null)
            {
                //敵との距離を比べ、一定距離離れていたらワープする
                RaycastHit hit;
                Vector3 dirs = _playerControl.EnemyCheck.NowEnemy.transform.position - _playerControl.PlayerMidlePos.position;
                Physics.Raycast(_playerControl.PlayerMidlePos.position, dirs.normalized, out hit, 20, _enemyLayer);
                float dis = Vector3.Distance(_playerControl.PlayerMidlePos.position, hit.point);

                if (dis >= _minDistanceWarpIsTargetting)
                {
                    //プレイヤーをワープ後の位置に移動
                    _playerControl.PlayerT.position = _targetPos;
                }
            }
            else
            {
                //プレイヤーをワープ後の位置に移動
                _playerControl.PlayerT.position = _targetPos;
            }
        }
        else
        {
            //プレイヤーをワープ後の位置に移動
            _playerControl.PlayerT.position = _targetPos;
        }

        if (_isTarget) return;

        //加える方向
        Vector3 dir = _targetPos - _playerControl.PlayerT.position;
        //加える力
        float speed = _playerControl.GroundCheck.IsHit() ? _endAddSpeedOnGround : _endAddSpeedOnAir;

        //プレイヤーを正面に少し移動させる
        _playerControl.Rb.velocity = _moveDir * speed;
    }


    /// <summary>投擲した武器の移動処理</summary>
    public void WeaponMove()
    {
        //加える方向
        Vector3 dir = _targetPos - _playerControl.PlayerT.position;

        //武器に速度を加える
        _weaponRb.velocity = dir.normalized * _shiftBreakSetting.MoveSpeed;
    }

    /// <summary>残像等の。ワープ時の演出の設定</summary>
    public void AfterImageStarting()
    {
        _isUseEffeck = true;

        //残像を出す
        _playerControl.AfterImage.PlayAfterImage(_shiftBreakSetting.ShiftBreakAnimName, _startPos, _playerControl.PlayerT.rotation);

        //透明にする
        _playerControl.MaterialControl.ChangeMaterial(MaterialControl.PlayerMaterialType.Clear);
    }

    /// <summary>シフトブレイク終了時の設定</summary>
    public void EndShifrBreak()
    {

        //パーティクル
        _playerControl.ParticleControl.ShiftBreakParticle(false);

        _isCanShiftBreak = false;
    }

    /// <summary>シフトブレイク攻撃終了時の設定</summary>
    public void ShiftBreakAttackEnd()
    {
        //重力をオン
        _playerControl.Rb.useGravity = true;

        _isShiftBreakNow = false;

        _isAttack = false;

        //投げた武器を非表示
        _playerControl.WeaponSetting.WeaponActives(false);
    }

    /// <summary>シフトブレイクのクールタイム計測</summary>
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
