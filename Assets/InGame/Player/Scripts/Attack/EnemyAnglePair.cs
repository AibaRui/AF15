using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyAnglePair
{
    /// <summary>Playerとの角度差</summary>
    private float _angle;

    /// <summary>敵のコライダー</summary>
    private Collider _collider;

    public float Angle => _angle;

    public Collider Collider => _collider;

    public EnemyAnglePair(Collider collider, float angle)
    {
        this._collider = collider;
        this._angle = angle;
    }
}
