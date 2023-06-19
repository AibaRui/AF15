using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class IdleState : PlayerStateBase
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

    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        //各クールタイム
        _stateMachine.PlayerController.Allways();

        _stateMachine.PlayerController.PickUp.Search();

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

        //  地上での移動
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
            }   //回避、構え

            if (_stateMachine.PlayerController.InputManager.IsAttack)
            {
                _stateMachine.TransitionTo(_stateMachine.AttackState);
                return;
            }   //攻撃


            if (_stateMachine.PlayerController.InputManager.IsJumping && _stateMachine.PlayerController.GroundCheck.IsHit())
            {
                _stateMachine.TransitionTo(_stateMachine.StateJump);
                return;
            }   //ジャンプ


            if (h != 0 || v != 0)
            {
                if (_stateMachine.PlayerController.InputManager.IsLeftShift)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateRun);
                }   //走りを押していたら走る
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateWalk);
                }  //押していなかったら歩く
            }   //移動
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
