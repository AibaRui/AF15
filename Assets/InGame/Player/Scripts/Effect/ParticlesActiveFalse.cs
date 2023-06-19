using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesActiveFalse : MonoBehaviour
{
    [Header("�I�u�W�F�N�g�ɂ��Ă��邷�ׂẴp�[�e�B�N��")]
    [SerializeField] private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();

    void Update()
    {

        //�S�ăp�[�e�B�N���̍Đ����I������炱�̃I�u�W�F�N�g���\���ɂ���
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
