using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    [SerializeField] private EnemyControl _enemyControl;


    public void EndAttack()
    {
        _enemyControl.EnemyAttack.EndAttack();
    }

    public void Dead()
    {
        _enemyControl.DeadDestoroy();
    }

}
