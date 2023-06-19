using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class PickUp
{
    [Header("�A�C�e�����E���A�j���[�V������")]
    [SerializeField] private string _animName;

    [Header("�A�C�e���̃��C���[")]
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

    /// <summary>�A�C�e�����E���n�߂�</summary>
    public void StartPickUp()
    {
        _playerControl.SoundManager.PlaySound(AudioType.ItemGet);

        _isPickUp = false;

        //�A�j���[�V�����Đ�
        _playerControl.AnimControl.PlayerAnimPlay(_animName);

        //�A�C�e���̎擾�����݂�
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


    /// <summary>�A�C�e�����E���I�����</summary>
    public void EndPickUp()
    {
        _isPickUp = true;
    }


}
