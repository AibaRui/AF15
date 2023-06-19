using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunObject : MonoBehaviour, IEffectable
{
    [SerializeField] private List<ParticleSystem> _muzzleFlash = new List<ParticleSystem>();

    public void UseEffect()
    {
        foreach (var p in _muzzleFlash)
        {
            p.Clear();

            p.Play();
        }
    }
}
