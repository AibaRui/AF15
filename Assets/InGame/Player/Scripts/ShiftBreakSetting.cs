using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShiftBreakSetting 
{
    [Header("シフトブレイクの名前")]
    [SerializeField] private string _shiftBreakAnimName;

    [Header("シフトブレイクの攻撃の名前")]
    [SerializeField] private string _shiftBreakAttackAnimName;

    [Header("武器の移動速度")]
    [SerializeField] private float _moveSpeed = 10;

    [Header("ターゲットなしの時の最大距離")]
    [SerializeField] private float _maxMoveDistanceOnNoTarget = 10;

    public string ShiftBreakAnimName => _shiftBreakAnimName;
    public string ShiftBreakAttackAnimName => _shiftBreakAttackAnimName;

    public float MoveSpeed => _moveSpeed;
    public float MaxMoveDistance => _maxMoveDistanceOnNoTarget;
}
