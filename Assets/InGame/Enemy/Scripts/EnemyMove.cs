using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyMove
{
    [Header("à⁄ìÆë¨ìx")]
    [SerializeField] private float _moveSpeed;

    [Header("èdóÕ")]
    [SerializeField] private float _gravity;

    [Header("çUåÇÇ…à⁄ÇÈÇ‹Ç≈ÇÃãóó£")]
    [SerializeField] private float _approti;

    [Header("âÒì]ë¨ìx")]
    [SerializeField] private float _rotateSpeed = 200;

    private Vector3 _playerDir;

    private EnemyControl _enemyControl;

    public void Init(EnemyControl enemyControl)
    {
        _enemyControl = enemyControl;
    }

    public void Gravity()
    {
        _enemyControl.Rb.AddForce(Vector3.down *_gravity);
    }

    public void SetDir()
    {
        _playerDir = (_enemyControl.BattleArea.PlayerControl.PlayerT.position - _enemyControl.EnemyT.position).normalized;
    }

    public void Rotate()
    {
        Quaternion _targetRotation = Quaternion.LookRotation(_playerDir, Vector3.up);

        Quaternion setR = Quaternion.RotateTowards(_enemyControl.EnemyT.rotation, _targetRotation, Time.deltaTime * _rotateSpeed);
        setR.x = 0;
        setR.z = 0;
        _enemyControl.EnemyT.rotation = setR;
    }

    public void Move()
    {
        Vector3 dir = _playerDir;
        dir.y = 0;

        _enemyControl.Rb.velocity = dir * _moveSpeed;

        if (Vector3.Distance(_enemyControl.BattleArea.PlayerControl.PlayerT.position, _enemyControl.EnemyT.position) < _approti)
        {
            _enemyControl.Rb.velocity = Vector3.zero;
            _enemyControl.EnemyAttack.Attack();
        }
    }


}
