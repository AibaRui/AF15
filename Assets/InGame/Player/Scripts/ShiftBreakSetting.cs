using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShiftBreakSetting 
{
    [Header("�V�t�g�u���C�N�̖��O")]
    [SerializeField] private string _shiftBreakAnimName;

    [Header("�V�t�g�u���C�N�̍U���̖��O")]
    [SerializeField] private string _shiftBreakAttackAnimName;

    [Header("����̈ړ����x")]
    [SerializeField] private float _moveSpeed = 10;

    [Header("�^�[�Q�b�g�Ȃ��̎��̍ő勗��")]
    [SerializeField] private float _maxMoveDistanceOnNoTarget = 10;

    public string ShiftBreakAnimName => _shiftBreakAnimName;
    public string ShiftBreakAttackAnimName => _shiftBreakAttackAnimName;

    public float MoveSpeed => _moveSpeed;
    public float MaxMoveDistance => _maxMoveDistanceOnNoTarget;
}
