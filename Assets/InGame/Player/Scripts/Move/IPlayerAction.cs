using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerAction 
{


    protected PlayerControl _playerControl = null;

    /// <summary>StateMacine���Z�b�g����֐�</summary>
    /// <param name="stateMachine"></param>
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;

    }
}
