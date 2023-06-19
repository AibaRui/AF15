using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerAction 
{


    protected PlayerControl _playerControl = null;

    /// <summary>StateMacineをセットする関数</summary>
    /// <param name="stateMachine"></param>
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;

    }
}
