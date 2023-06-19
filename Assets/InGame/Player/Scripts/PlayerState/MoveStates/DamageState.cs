using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.Damage.FirstSetting();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.Damage.EndDamage();
    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.Damage.PlayerRotate();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        _stateMachine.PlayerController.Allways();

        if (_stateMachine.PlayerController.Damage.IsEndDown && _stateMachine.PlayerController.GroundCheck.IsHit())
        {
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                return;
            }
        }
    }
}
