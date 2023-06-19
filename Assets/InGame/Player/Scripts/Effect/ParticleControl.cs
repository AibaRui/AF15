using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    [Header("���[�v���̃p�[�e�B�N��")]
    [SerializeField] private ParticleSystem[] _Warpparticle;

    [Header("�V�t�g�u���C�N���̃p�[�e�B�N��")]
    [SerializeField] private ParticleSystem[] _shiftBreakParticle;

    [Header("����؂�ւ����̃p�[�e�B�N��")]
    [SerializeField] private ParticleSystem _changeWeaponParticle;

    [Header("�U��Hit���̃p�[�e�B�N��")]
    [SerializeField] private AttackHitParticles _attackHitParticles;

    [SerializeField] private List<ParticleSystem> _healParticle = new List<ParticleSystem>();

    [SerializeField] private PlayerControl _playerControl;

    [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();


    public AttackHitParticles AttackHitParticles => _attackHitParticles;

    private void Awake()
    {
        _attackHitParticles.Init(this);
    }

    public GameObject InstansiateObject(GameObject obj)
    {
        return Instantiate(obj);
    }

    public void HealParticle()
    {
        foreach (var a in _healParticle)
        {
            a.loop = false;
            a.Play();
        }
    }


    public void WarpParticle(bool isUse)
    {
        if (isUse)
        {
            foreach (var a in _Warpparticle)
            {
                particleSystems.Add(a);
                a.loop = true;
                a.Play();
            }
        }
        else
        {
            foreach (var a in _Warpparticle)
            {
                a.loop = false;
                a.Stop();
            }
        }
    }

    public void ShiftBreakParticle(bool isUse)
    {
        if (isUse)
        {
            foreach (var a in _shiftBreakParticle)
            {
                a.Clear();
            }

            foreach (var a in _shiftBreakParticle)
            {
                a.loop = true;
                a.Play();
            }
        }
        else
        {
            foreach (var a in _shiftBreakParticle)
            {
                a.loop = false;
                a.Stop();
            }
        }
    }


}
