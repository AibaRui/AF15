using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackHitParticles
{
    [Header("剣のエフェクト")]
    [SerializeField] private AttackHitEffectBase _sowrd;

    [Header("大剣のエフェクト")]
    [SerializeField] private AttackHitEffectBase _largeSowrd;

    [Header("槍のエフェクト")]
    [SerializeField] private AttackHitEffectBase _spear;

    [Header("弓のエフェクト")]
    [SerializeField] private AttackHitEffectBase _bow;

    [Header("銃のエフェクト")]
    [SerializeField] private AttackHitEffectBase _gun;

    [Header("双剣のエフェクト")]
    [SerializeField] private AttackHitEffectBase _twinSword;

    /// <summary>生成したエフェクト達</summary>
    private List<ParticlesObject> _objects = new List<ParticlesObject>();

    private ParticleControl _particleControl;

    public void Init(ParticleControl particleControl)
    {
        _particleControl = particleControl;
    }



    public void UseAttackHitEffect(Vector3 pos, AllWeapons.WeaponType weaponType, HitType hitType)
    {
        if (_objects.Count > 0)
        {
            foreach (var obj in _objects)
            {
                if (obj.WeaponType == weaponType && obj.Type == hitType && !obj.Object.activeSelf)
                {
                    obj.Object.transform.position = pos;
                    obj.Object.SetActive(true);
                    return;
                }
            }
        }

        //無い、もしくは使用中は新しく生成して使用
        var particle = InsParticle(weaponType, hitType);

        particle.Object.transform.position = pos;
        particle.Object.SetActive(true);
    }


    /// <summary>指定したパーティクルを生成する</summary>
    /// <param name="weaponType">武器の種類</param>
    /// <param name="hitType">Hitの種類</param>
    public ParticlesObject InsParticle(AllWeapons.WeaponType weaponType, HitType hitType)
    {
        AttackHitEffectBase effectBase = default;

        if (weaponType == AllWeapons.WeaponType.Sword)
        {
            effectBase = _sowrd;
        }
        else if (weaponType == AllWeapons.WeaponType.LargeSword)
        {
            effectBase = _largeSowrd;
        }
        else if (weaponType == AllWeapons.WeaponType.Spear)
        {
            effectBase = _spear;
        }
        else if (weaponType == AllWeapons.WeaponType.Gun)
        {
            effectBase = _gun;
        }
        else if(weaponType ==AllWeapons.WeaponType.twinSword)
        {
            effectBase = _twinSword;
        }

        GameObject efftctObj = default;

        if (hitType == HitType.Nomal)
        {
            efftctObj = effectBase.Nomal;
        }
        else if (hitType == HitType.Clitical)
        {
            efftctObj = effectBase.Critical;
        }
        else if (hitType == HitType.Fire)
        {
            efftctObj = effectBase.Fire;
        }
        else if (hitType == HitType.Ice)
        {
            efftctObj = effectBase.Ice;
        }
        else if (hitType == HitType.Thunder)
        {
            efftctObj = effectBase.Thunder;
        }

        var go = _particleControl.InstansiateObject(efftctObj);

        ParticlesObject newP = new ParticlesObject { Object = go, Type = hitType, WeaponType = weaponType };

        newP.Object.SetActive(false);

        _objects.Add(newP);

        return newP;
    }


}

public class ParticlesObject
{
    public GameObject Object { get; set; }
    public HitType Type { get; set; }

    public AllWeapons.WeaponType WeaponType { get; set; }

}

/// <summary> Hitの種類</summary>
public enum HitType
{
    Nomal,
    Clitical,
    Fire,
    Ice,
    Thunder,
}