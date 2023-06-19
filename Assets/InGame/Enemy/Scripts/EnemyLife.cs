using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyLife
{
    private float _nowHp;

    public float NowHp => _nowHp;

    private bool _isDamage;

    private EnemyControl _enemyControl;

    public void Init(EnemyControl enemyControl)
    {
        _enemyControl = enemyControl;

        _nowHp = _enemyControl.EnemyData.MaxHp;
    }

    public void ReSet()
    {
        _isDamage = false;
    }

    public bool Damage(float damage, Attribute attribute, AllWeapons.WeaponType type)
    {
        float addDamage = damage;

        if (_enemyControl.EnemyData.WeakAttributes.Contains(attribute))
        {
            addDamage += damage * 0.2f;
        }

        if (_enemyControl.EnemyData.WeakWeapons.Contains(type))
        {
            addDamage += damage * 0.2f;
        }

        //Hp‚ðŒ¸‚ç‚·
        _nowHp -= damage;

        if (_nowHp <= 0)
        {
            Debug.Log("Damage");
            return true;
        }
        return false;
    }

}
