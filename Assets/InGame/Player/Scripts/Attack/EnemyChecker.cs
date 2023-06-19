using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyChecker : MonoBehaviour
{
    [Header("敵の探知範囲")]
    [SerializeField] private float _radius = 5;

    [Header("敵の探知レイヤー")]
    [SerializeField] private LayerMask _layer;

    [Header("障害物とみなすすべてのレイヤー")]
    [SerializeField] private LayerMask _obstacleLayer;

    [Header("戦闘エリアのレイヤー")]
    [SerializeField] private LayerMask _buttuleErea;

    [SerializeField] private PlayerControl _playerControl;

    /// <summary>現在ターゲッティングしているかどうか </summary>
    private bool _isTarget;

    private bool _isInBattleArea;

    private Collider _buttuleAreaCollider;


    /// <summary>現在ロックオンしている敵</summary>
    private GameObject _nowLockOnEnemy;

    private List<GameObject> _obj = new List<GameObject>();

    public List<GameObject> RockOns => _obj;
    public bool IsInButtuleArea => _isInBattleArea;
    public bool IsTargetting => _isTarget;

    public GameObject NowEnemy => _nowLockOnEnemy;


    /// <summaryターゲティングしているかどうかを決める</summary>
    public void EnemyTargetting()
    {
        if (_playerControl.InputManager.IsRightMouseClickDown && _nowLockOnEnemy != null)
        {
            _isTarget = true;
        }   //ターゲティングボタンを押していたらターゲティング中のする

        if (_playerControl.InputManager.IsRightMouseClickUp)
        {
            _isTarget = false;
        }   //ターゲティングボタンを押して居なかったらターゲティング中にしない
    }

    /// <summary>敵を探知する</summary>
    public void EnemyCheck()
    {
        CheckEnemy();
        ObstacleCheck();
        SearchButtuleArea();
    }

    public void SearchButtuleArea()
    {
        var a = _playerControl.ColliderSearcher.Search(_buttuleErea);

        if (a.Length > 0)
        {
            if (_buttuleAreaCollider == null)
            {
                _isInBattleArea = true;
                _buttuleAreaCollider = a[0];
                _buttuleAreaCollider.GetComponent<IInButtuleAreable>()?.InPlayer(_playerControl);
            }
        }
        else
        {
            if (_buttuleAreaCollider != null)
            {
                _isInBattleArea = false;
                _buttuleAreaCollider.GetComponent<IInButtuleAreable>()?.OutPlayer();
                _buttuleAreaCollider = null;
            }
        }
    }

    /// <summary>ロックオンしている敵までの障害物を確認する</summary>
    public void ObstacleCheck()
    {
        RaycastHit hitObstacle;

        if (_nowLockOnEnemy == null || !_isInBattleArea) return;

        //ロックオンしている敵に向けてRayを飛ばす
        bool hit = Physics.Linecast(_playerControl.PlayerMidlePos.position, _nowLockOnEnemy.transform.position, out hitObstacle, _obstacleLayer);

        if (hit)
        {
            //取得したコライダーの一番上の親オブジェクトを取得
            var nowEnemyParent = _nowLockOnEnemy.transform.root.gameObject;

            //ロックオンしている敵の一番上の親オブジェクトを取得
            var hitEnemyParent = hitObstacle.collider.gameObject.transform.root.gameObject;

            //双方が同じでない場合、ロックオンしている敵との間に障害物が
            //あるとみてロックオンを解除する
            if (hit && nowEnemyParent != hitEnemyParent) _nowLockOnEnemy = null;
        }
    }

    /// <summary>敵がいるかどうかを確認する</summary>
    public void CheckEnemy()
    {
        //1.敵を検出
        //2.プレイヤーの視点の先を正面として、右回りに角度差を計算していく
        //3.角度差の小さいものをロックオン

        if (_isTarget || !_isInBattleArea) return;

        //敵を検出
        Collider[] getColliders = Physics.OverlapSphere(_playerControl.PlayerMidlePos.position, _radius, _layer);
        List<EnemyAnglePair> enemyAngles = new List<EnemyAnglePair>();



        //敵がいない
        if (getColliders.Length == 0)
        {
            _nowLockOnEnemy = null;
            return;
        }
        else
        {

        }

        List<GameObject> enemys = new List<GameObject>();

        //敵とプレイヤーの角度を計算
        foreach (Collider collider in getColliders)
        {
            Vector3 direction = collider.transform.position - _playerControl.PlayerT.position;
            float angle = Vector3.Angle(_playerControl.PlayerT.forward, direction);
            enemyAngles.Add(new EnemyAnglePair(collider, angle));
            enemys.Add(collider.gameObject);
        }

        //角度差が低い順にソート
        enemyAngles.Sort((a, b) => a.Angle.CompareTo(b.Angle));

        //ロックオンする敵を設定
        _nowLockOnEnemy = enemyAngles[0].Collider.gameObject;

        _obj = enemys;
    }
}
