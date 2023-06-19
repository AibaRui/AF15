using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "HitEffects")]
public class AttackHitEffectBase : ScriptableObject
{
    [Header("通常Hitのエフェクトの親")]
    [SerializeField] private GameObject _hitEffectObject;

    [Header("クリティカルHitのエフェクトの親")]
    [SerializeField] private GameObject _hitCliticalEffectObject;

    [Header("炎Hitのエフェクトの親")]
    [SerializeField] private GameObject _hitFireEffectObject;

    [Header("氷Hitのエフェクトの親")]
    [SerializeField] private GameObject _hitIceEffectObject;

    [Header("雷Hitのエフェクトの親")]
    [SerializeField] private GameObject _hitThunderEffectObject;


    public GameObject Nomal => _hitEffectObject;

    public GameObject Critical => _hitCliticalEffectObject;

    public GameObject Fire => _hitFireEffectObject;

    public GameObject Ice => _hitIceEffectObject;

    public GameObject Thunder => _hitThunderEffectObject;

}
