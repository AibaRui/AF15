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
        }   //�V�t�g�u���C�N

        //����ύX
        if (_stateMachine.PlayerController.WeaponSetting.IsWeaponChangeAnimation)
        {
            _stateMachine.TransitionTo(_stateMachine.WeaponSelectState);
            return;
        }

        //�U��
        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState);
            return;
        }


        //�ȉ��́A����������Ă��Ȃ����Ɉڍs�\
        if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
        {

            if (_stateMachine.PlayerController.HoldAvoidMove.IsDamage)
            {
                _stateMachine.PlayerController.Avoid.SetAvoidDir();
                _stateMachine.TransitionTo(_stateMachine.AvoidState);
                return;
            }   //���

            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                _stateMachine.TransitionTo(_stateMachine.StateholdAvoidldle);
                return;
            }   //�����A����\��
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateholdAvoidAir);
                return;
            }   //�󒆁A����\��
        }
        else
        {
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //�~�܂�
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

            //�㏸�A�~��
            if (!_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                if (_stateMachine.PlayerController.Rb.velocity.y > 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                }      //�㏸
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                }   //�~��
                return;
            }


        }

    }
}
