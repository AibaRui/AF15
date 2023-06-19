using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RunState : PlayerStateBase
{
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.Move.Move(PlayerMove.MoveType.Run);
    }

    public override void LateUpdate()
    {
        _stateMachine.PlayerController.PickUp.Search();
    }

    public override void Update()
    {
        _stateMachine.PlayerController.Allways();


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
        }   //攻撃

        if (_stateMachine.PlayerController.GroundCheck.IsHit())
        {
            if (_stateMachine.PlayerController.PickUp.IsCanPickUp && _stateMachine.PlayerController.InputManager.IsGetItem
                && !_stateMachine.PlayerController.EnemyCheck.IsInButtuleArea)
            {
                _stateMachine.TransitionTo(_stateMachine.StatePickUpItem);
                return;
            }            //アイテムを拾う



            var h = _stateMachine.PlayerController.InputManager.HorizontalInput;
            var v = _stateMachine.PlayerController.InputManager.VerticalInput;

            if (_stateMachine.PlayerController.Avoid.IsRoalInput && _stateMachine.PlayerController.Avoid.IsCanAvoid
                && (h != 0 || v != 0))
            {
                _stateMachine.PlayerController.Avoid.SetAvoidRoal();
                _stateMachine.PlayerController.Avoid.SetAvoidDir();
                _stateMachine.TransitionTo(_stateMachine.AvoidState);
                return;
            }     ///ロール回避
            //歩き
            if ((h != 0 || v != 0) && !_stateMachine.PlayerController.InputManager.IsLeftShift)
            {
                _stateMachine.TransitionTo(_stateMachine.StateWalk);
                return;
            }

            //止まる
            if (h == 0 && v == 0)
            {

                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                return;
            }

            if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
            {
                if (h != 0 || v != 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateholdAvoidWalk);
                    return;
                }
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateholdAvoidldle);
                    return;
                }
            }    //回避を押している時に移行可能



            //ジャンプ
            if (_stateMachine.PlayerController.InputManager.IsJumping && _stateMachine.PlayerController.GroundCheck.IsHit())
            {
                _stateMachine.TransitionTo(_stateMachine.StateJump);
            }
        }
    }
}

