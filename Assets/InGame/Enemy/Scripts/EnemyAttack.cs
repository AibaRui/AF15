using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack
{
    [Header("最小のクールタイム")]
    [SerializeField] private int _minCoolTime;

    [Header("最大のクールタイム")]
    [SerializeField] private int _maxCoolTime;

    private float _setCoolTime = 0;

    private float _countCoolTime = 0;

    private bool _isAttack = false;

    public bool IsAttack => _isAttack;
    public bool IsAttackCoolTIme => _isCoolTime;
    private bool _isCoolTime = false;

    private EnemyControl _enemyControl;

    public void Init(EnemyControl enemyControl)
    {
        _enemyControl = enemyControl;
    }

    public void Attack()
    {
        _isAttack = true;

        _isCoolTime = true;

        _setCoolTime = Random.Range(_minCoolTime, _maxCoolTime);

        //攻撃処理
        _enemyControl.AnimControl.AttackAnim();
    }

    public void CountCoolTime()
    {
        if (_isCoolTime && !_isAttack)
        {

            _countCoolTime += Time.deltaTime;

            if (_countCoolTime >= _setCoolTime)
            {
                _countCoolTime = 0;
                _isCoolTime = false;
            }
        }
    }


    public void EndAttack()
    {
        _isAttack = false;
    }

}
