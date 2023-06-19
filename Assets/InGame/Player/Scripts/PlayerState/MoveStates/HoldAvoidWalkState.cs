using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoldAvoidWalkState : PlayerStateBase
{
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.HoldAvoidMove.Move();
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

        //回避を押していない時に移行可能
        if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
        {
            if(_stateMachine.PlayerController.HoldAvoidMove.IsDamage)
            {
                _stateMachine.PlayerController.Avoid.SetAvoidDir();
                _stateMachine.TransitionTo(_stateMachine.AvoidState);
                return;
            }

            //  地上での移動
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //移動
                if (_stateMachine.PlayerController.InputManager.HorizontalInput == 0 && _stateMachine.PlayerController.InputManager.VerticalInput == 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateholdAvoidldle);
                }
            }
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateholdAvoidAir);
            }
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
            else
            {
                //上昇、降下
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
