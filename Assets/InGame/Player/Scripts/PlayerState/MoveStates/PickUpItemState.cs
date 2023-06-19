using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickUpItemState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.PickUp.StartPickUp();
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        if (_stateMachine.PlayerController.PickUp.IsPickUp)
        {
            _stateMachine.TransitionTo(_stateMachine.StateIdle);
        }
    }
}
