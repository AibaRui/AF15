using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AvoidState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.Avoid.SetSpeedLimit();

        _stateMachine.PlayerController.Avoid.AvoidStart();
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.Avoid.EndAvoid();
    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.Avoid.DoAvoid();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        _stateMachine.PlayerController.Allways();

        if (_stateMachine.PlayerController.Avoid.IsEndAvoid)
        {
            var h = _stateMachine.PlayerController.InputManager.HorizontalInput;
            var v = _stateMachine.PlayerController.InputManager.VerticalInput;

            //âÒîÇâüÇµÇƒÇ¢ÇÈéûÇ…à⁄çsâ¬î\
            if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
            {
                if (h != 0 || v != 0)
                {
                    Debug.Log("WALK");
                    _stateMachine.TransitionTo(_stateMachine.StateholdAvoidWalk);
                    return;
                }
                else
                {
                    Debug.Log("Idle");
                    _stateMachine.TransitionTo(_stateMachine.StateholdAvoidldle);
                    return;
                }
            }

            //  ínè„Ç≈ÇÃà⁄ìÆ
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                if (_stateMachine.PlayerController.InputManager.HorizontalInput != 0 || _stateMachine.PlayerController.InputManager.VerticalInput != 0)
                {
                    if (_stateMachine.PlayerController.InputManager.IsSwing == 1)
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateRun);
                    }   //ëñÇËÇâüÇµÇƒÇ¢ÇΩÇÁëñÇÈ
                    else
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    }  //âüÇµÇƒÇ¢Ç»Ç©Ç¡ÇΩÇÁï‡Ç≠
                }
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateIdle);
                }   //óßÇøèÛë‘


            }


            //ãÛíÜÇ…Ç¢ÇÈÇ∆Ç´
            if (!_stateMachine.PlayerController.GroundCheck.IsHit())
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
