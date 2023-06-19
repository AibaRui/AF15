using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpAirState : PlayerStateBase
{
    public override void Enter()
    {

        _stateMachine.PlayerController.VelocityLimit.SetLimit(25, 20, 25);
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.Move.ReSetTime();

    }

    public override void FixedUpdate()
    {
        if (_stateMachine.PlayerController.InputManager.HorizontalInput != 0 || _stateMachine.PlayerController.InputManager.VerticalInput != 0)
            _stateMachine.PlayerController.Move.AirMove();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        //各動作のクールタイム
        _stateMachine.PlayerController.Allways();

        var h = _stateMachine.PlayerController.InputManager.HorizontalInput;
        var v = _stateMachine.PlayerController.InputManager.VerticalInput;

        if (_stateMachine.PlayerController.Damage.IsDamage)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDamage);
            return;
        }   //ダメージを受けた

        if (_stateMachine.PlayerController.InputManager.IsShiftBreak && _stateMachine.PlayerController.ShiftBreak.IsCanShiftBreak)
        {
            _stateMachine.TransitionTo(_stateMachine.ShiftBreakState);
            return;
        }   //シフトブレイク

    
        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState);
            return;
        }    //攻撃

    
        if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
        {
            _stateMachine.TransitionTo(_stateMachine.StateholdAvoidAir);
            return;
        }    //回避を押している時に移行可能

        if (_stateMachine.PlayerController.GroundCheck.IsHit())
        {
            //_stateMachine.TransitionTo(_stateMachine.StateIdle);
            return;

        }   //地面
        else
        {
            if (_stateMachine.PlayerController.Rb.velocity.y <= 0)
            {
                _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                return;
            }
        }






        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            if (_stateMachine.PlayerController.Attack.IsCanAttack)
            {
                _stateMachine.TransitionTo(_stateMachine.AttackState);
            }
        }   //攻撃ステート
    }
}
