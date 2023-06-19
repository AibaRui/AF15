using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesActiveFalse : MonoBehaviour
{
    [Header("オブジェクトについているすべてのパーティクル")]
    [SerializeField] private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();

    void Update()
    {

        //全てパーティクルの再生が終わったらこのオブジェクトを非表示にする
        int count = 0;

        foreach (var particle in _particleSystems)
        {
            if (!particle.isPlaying)
            {
                count++;
            }
        }

        if (count == _particleSystems.Count)
        {
            gameObject.SetActive(false);
        }

    }
}
