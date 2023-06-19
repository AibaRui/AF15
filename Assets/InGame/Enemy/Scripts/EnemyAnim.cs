using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAnim
{
    [Header("�A�j���[�^�[��")]
    [SerializeField] private int _maxAttackNum;

    [Header("AttackTrigger��")]
    [SerializeField] private string _attackTriggerName;

    [Header("AttackType��")]
    [SerializeField] private string _attackType;

    [Header("�X�s�[�h")]
    [SerializeField] private string _moveSpeed;

    [Header("���񂾎��̃A�j���[�V������")]
 [SerializeField]   private string _deathName;

    private EnemyControl _enemyControl;

    public void Init(EnemyControl enemyControl)
    {
        _enemyControl = enemyControl;
    }


    public void AnimParametaSet()
    {

        _enemyControl.Animator.SetFloat(_moveSpeed,_enemyControl.Rb.velocity.magnitude);
    }

    public void AttackAnim()
    {
        int r = Random.Range(1, _maxAttackNum + 1);

        _enemyControl.Animator.SetInteger(_attackType, r);

        _enemyControl.Animator.SetTrigger(_attackTriggerName);
    }

    public void DeathAnim()
    {
        _enemyControl.Animator.Play(_deathName);
    }

}
