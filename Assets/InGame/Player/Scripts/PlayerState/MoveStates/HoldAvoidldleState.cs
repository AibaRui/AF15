using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoldAvoidldleState : PlayerStateBase
{
    public override void Enter()
    {

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
        //各クールタイム
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

        //回避を押している時に移行可能
        if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
        {
            if (_stateMachine.PlayerController.HoldAvoidMove.IsDamage)
            {
                _stateMachine.PlayerController.Avoid.SetAvoidDir();
                _stateMachine.TransitionTo(_stateMachine.AvoidState);
                return;
            }  　//回避

            //  地上での移動
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                if (_stateMachine.PlayerController.InputManager.HorizontalInput != 0 || _stateMachine.PlayerController.InputManager.VerticalInput != 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateholdAvoidWalk);
                }    //回避、構え移動
            }
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateholdAvoidAir);
            }   //回避、空中構え
        }
        else
        {
            //  地上での移動
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //移動
                if (_stateMachine.PlayerController.InputManager.HorizontalInput != 0 || _stateMachine.PlayerController.InputManager.VerticalInput != 0)
                {
                    if (_stateMachine.PlayerController.InputManager.IsLeftShift)
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateRun);
                    }   //走りを押していたら走る
                    else
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    }  //押していなかったら歩く
                }
            }

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
            }
        }


    }
}
