using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArea : MonoBehaviour, IInButtuleAreable
{


    private bool _isBattle = false;

    public bool IsBattle => _isBattle;

    private PlayerControl _playerControl;
    public PlayerControl PlayerControl => _playerControl;

    public void InPlayer(PlayerControl player)
    { 
        _isBattle = true;
        _playerControl = player;
    }

    public void OutPlayer()
    {
        Debug.Log("Out");
        _isBattle = false;
        _playerControl = null;
    }


}
