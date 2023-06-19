using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDamageble 
{
    void AddDamageEnemy(float damage, AllWeapons.WeaponType type, Attribute attribute);
}
