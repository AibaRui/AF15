using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSearcher : MonoBehaviour
{
    [Header("’PˆÊ”ÍˆÍ")]
    [SerializeField] private Vector3 _searchSize;

    [Header("’†S")]
    [SerializeField] private Vector3 _center;

    [SerializeField] private PlayerControl _playerControl;


    public Collider[] Search(LayerMask _layer)
    {
        return Physics.OverlapBox(_playerControl.PlayerT.position + _center, _searchSize, Quaternion.identity, _layer);
    }

}
