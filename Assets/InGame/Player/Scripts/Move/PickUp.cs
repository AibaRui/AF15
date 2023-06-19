using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class PickUp
{
    [Header("アイテムを拾うアニメーション名")]
    [SerializeField] private string _animName;

    [Header("アイテムのレイヤー")]
    [SerializeField] private LayerMask _itemLayer;

    private Collider _colliders;

    private bool _isCanPickUp = false;

    private bool _isPickUp = false;

    private PlayerControl _playerControl;
    public Collider Collider => _colliders;


    public bool IsCanPickUp => _isCanPickUp;
    public bool IsPickUp => _isPickUp;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>アイテムを拾い始めた</summary>
    public void StartPickUp()
    {
        _playerControl.SoundManager.PlaySound(AudioType.ItemGet);

        _isPickUp = false;

        //アニメーション再生
        _playerControl.AnimControl.PlayerAnimPlay(_animName);

        //アイテムの取得を試みる
        _colliders.GetComponent<IGetable>()?.PickUpItem();
    }

    public void Search()
    {
        Collider[] colliders = _playerControl.ColliderSearcher.Search(_itemLayer);

        if (colliders.Length > 0)
        {
            _colliders = colliders[0];
        }
        else
        {
            _colliders = null;
        }

        if (colliders.Length > 0)
        {
            _isCanPickUp = true;
        }
        else
        {
            _isCanPickUp = false;
        }
    }


    /// <summary>アイテムを拾い終わった</summary>
    public void EndPickUp()
    {
        _isPickUp = true;
    }


}
