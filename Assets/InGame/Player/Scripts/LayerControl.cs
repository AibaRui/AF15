using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class LayerControl 
{
    [SerializeField] private string _layerAvoid;

    [SerializeField] private string _layerPlayer;


    private PlayerControl _playerControl;

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    /// <summary>通常のレイヤーに設定</summary>
    public void SetPlayerDefaltLayer()
    {
        _playerControl.Player.layer = LayerMask.NameToLayer(_layerPlayer);
    }

    /// <summary>無敵のレイヤーに設定</summary>
    public void SetPlayerGodLayer()
    {
        _playerControl.Player.layer = LayerMask.NameToLayer(_layerAvoid);
    }

}
