using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "HitEffects")]
public class AttackHitEffectBase : ScriptableObject
{
    [Header("�ʏ�Hit�̃G�t�F�N�g�̐e")]
    [SerializeField] private GameObject _hitEffectObject;

    [Header("�N���e�B�J��Hit�̃G�t�F�N�g�̐e")]
    [SerializeField] private GameObject _hitCliticalEffectObject;

    [Header("��Hit�̃G�t�F�N�g�̐e")]
    [SerializeField] private GameObject _hitFireEffectObject;

    [Header("�XHit�̃G�t�F�N�g�̐e")]
    [SerializeField] private GameObject _hitIceEffectObject;

    [Header("��Hit�̃G�t�F�N�g�̐e")]
    [SerializeField] private GameObject _hitThunderEffectObject;


    public GameObject Nomal => _hitEffectObject;

    public GameObject Critical => _hitCliticalEffectObject;

    public GameObject Fire => _hitFireEffectObject;

    public GameObject Ice => _hitIceEffectObject;

    public GameObject Thunder => _hitThunderEffectObject;

}
