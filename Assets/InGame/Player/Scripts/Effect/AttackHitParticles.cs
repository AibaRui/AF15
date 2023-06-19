using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackHitParticles
{
    [Header("���̃G�t�F�N�g")]
    [SerializeField] private AttackHitEffectBase _sowrd;

    [Header("�匕�̃G�t�F�N�g")]
    [SerializeField] private AttackHitEffectBase _largeSowrd;

    [Header("���̃G�t�F�N�g")]
    [SerializeField] private AttackHitEffectBase _spear;

    [Header("�|�̃G�t�F�N�g")]
    [SerializeField] private AttackHitEffectBase _bow;

    [Header("�e�̃G�t�F�N�g")]
    [SerializeField] private AttackHitEffectBase _gun;

    [Header("�o���̃G�t�F�N�g")]
    [SerializeField] private AttackHitEffectBase _twinSword;

    /// <summary>���������G�t�F�N�g�B</summary>
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

        //�����A�������͎g�p���͐V�����������Ďg�p
        var particle = InsParticle(weaponType, hitType);

        particle.Object.transform.position = pos;
        particle.Object.SetActive(true);
    }


    /// <summary>�w�肵���p�[�e�B�N���𐶐�����</summary>
    /// <param name="weaponType">����̎��</param>
    /// <param name="hitType">Hit�̎��</param>
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

/// <summary> Hit�̎��</summary>
public enum HitType
{
    Nomal,
    Clitical,
    Fire,
    Ice,
    Thunder,
}