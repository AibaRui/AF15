using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WalkState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.Step.ResetStep();
        _stateMachine.PlayerController.Attack.IsCanAttack = true;
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.Move.Move(PlayerMove.MoveType.Walk);

    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        _stateMachine.PlayerController.Allways();

        _stateMachine.PlayerController.PickUp.Search();

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


        if (_stateMachine.PlayerController.GroundCheck.IsHit())
        {
            if (_stateMachine.PlayerController.PickUp.IsCanPickUp && _stateMachine.PlayerController.InputManager.IsGetItem
                && !_stateMachine.PlayerController.EnemyCheck.IsInButtuleArea)
            {
                _stateMachine.TransitionTo(_stateMachine.StatePickUpItem);
                return;
            }   //アイテムを拾う

            if (_stateMachine.PlayerController.WeaponSetting.IsWeaponChangeAnimation)
            {
                _stateMachine.TransitionTo(_stateMachine.WeaponSelectState);
                return;
            }   //武器変更

            if (_stateMachine.PlayerController.InputManager.IsAttack)
            {
                _stateMachine.TransitionTo(_stateMachine.AttackState);
                return;
            }  //攻撃

            var h = _stateMachine.PlayerController.InputManager.HorizontalInput;
            var v = _stateMachine.PlayerController.InputManager.VerticalInput;


            if (_stateMachine.PlayerController.Avoid.IsRoalInput && _stateMachine.PlayerController.Avoid.IsCanAvoid
                && (h != 0 || v != 0))
            {
                _stateMachine.PlayerController.Avoid.SetAvoidRoal();
                _stateMachine.PlayerController.Avoid.SetAvoidDir();
                _stateMachine.TransitionTo(_stateMachine.AvoidState);
                return;
            }   ///回避

            //回避を押している時に移行可能
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
            }


            //走り
            if ((h != 0 || v != 0)
                && _stateMachine.PlayerController.InputManager.IsLeftShift)
            {
                _stateMachine.TransitionTo(_stateMachine.StateRun);
                return;
            }

            //止まる
            if (_stateMachine.PlayerController.InputManager.HorizontalInput == 0 && _stateMachine.PlayerController.InputManager.VerticalInput == 0)
            {
                _stateMachine.TransitionTo(_stateMachine.StateIdle);
                return;
            }

            //ジャンプ
            if (_stateMachine.PlayerController.InputManager.IsJumping && _stateMachine.PlayerController.GroundCheck.IsHit())
            {
                _stateMachine.TransitionTo(_stateMachine.StateJump);
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

