using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoldAvoidAirState : PlayerStateBase
{
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.HoldAvoidMove.AirMove();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        _stateMachine.PlayerController.Allways();

        if (_stateMachine.PlayerController.InputManager.IsShiftBreak && _stateMachine.PlayerController.ShiftBreak.IsCanShiftBreak)
        {
            _stateMachine.TransitionTo(_stateMachine.ShiftBreakState);
            return;
        }   //シフトブレイク

        //武器変更
        if (_stateMachine.PlayerController.WeaponSetting.IsWeaponChangeAnimation)
        {
            _stateMachine.TransitionTo(_stateMachine.WeaponSelectState);
            return;
        }

        //攻撃
        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState);
            return;
        }


        //以下は、回避を押していない時に移行可能
        if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
        {

            if (_stateMachine.PlayerController.HoldAvoidMove.IsDamage)
            {
                _stateMachine.PlayerController.Avoid.SetAvoidDir();
                _stateMachine.TransitionTo(_stateMachine.AvoidState);
                return;
            }   //回避

            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                _stateMachine.TransitionTo(_stateMachine.StateholdAvoidldle);
                return;
            }   //歩き、回避構え
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateholdAvoidAir);
                return;
            }   //空中、回避構え
        }
        else
        {
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //止まる
                if (_stateMachine.PlayerController.InputManager.HorizontalInput == 0 && _stateMachine.PlayerController.InputManager.VerticalInput == 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateIdle);
                    return;
                }
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    return;
                }
            }

            //上昇、降下
            if (!_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                if (_stateMachine.PlayerController.Rb.velocity.y > 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                }      //上昇
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                }   //降下
                return;
            }


        }

    }
}
