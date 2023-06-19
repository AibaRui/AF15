using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyAnglePair
{
    /// <summary>Player�Ƃ̊p�x��</summary>
    private float _angle;

    /// <summary>�G�̃R���C�_�[</summary>
    private Collider _collider;

    public float Angle => _angle;

    public Collider Collider => _collider;

    public EnemyAnglePair(Collider collider, float angle)
    {
        this._collider = collider;
        this._angle = angle;
    }
}
