using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ShiftBreakState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.ShiftBreak.StartShiftBreak();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.ShiftBreak.EndShifrBreak();
    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.ShiftBreak.MoveShiftBreak();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        _stateMachine.PlayerController.Allways();

        if (!_stateMachine.PlayerController.ShiftBreak.IsShiftBreake)
        {
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //Idle
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
            }
            else
            {

                if (_stateMachine.PlayerController.Rb.velocity.y > 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                }   //è„è∏
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                }   //ç~â∫
            }
        }
    }
}
