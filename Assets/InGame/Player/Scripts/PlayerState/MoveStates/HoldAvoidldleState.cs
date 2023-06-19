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
        //�e�N�[���^�C��
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

        //����������Ă��鎞�Ɉڍs�\
        if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
        {
            if (_stateMachine.PlayerController.HoldAvoidMove.IsDamage)
            {
                _stateMachine.PlayerController.Avoid.SetAvoidDir();
                _stateMachine.TransitionTo(_stateMachine.AvoidState);
                return;
            }  �@//���

            //  �n��ł̈ړ�
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                if (_stateMachine.PlayerController.InputManager.HorizontalInput != 0 || _stateMachine.PlayerController.InputManager.VerticalInput != 0)
                {
                    _stateMachine.TransitionTo(_stateMachine.StateholdAvoidWalk);
                }    //����A�\���ړ�
            }
            else
            {
                _stateMachine.TransitionTo(_stateMachine.StateholdAvoidAir);
            }   //����A�󒆍\��
        }
        else
        {
            //  �n��ł̈ړ�
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //�ړ�
                if (_stateMachine.PlayerController.InputManager.HorizontalInput != 0 || _stateMachine.PlayerController.InputManager.VerticalInput != 0)
                {
                    if (_stateMachine.PlayerController.InputManager.IsLeftShift)
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateRun);
                    }   //����������Ă����瑖��
                    else
                    {
                        _stateMachine.TransitionTo(_stateMachine.StateWalk);
                    }  //�����Ă��Ȃ����������
                }
            }

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
            }
        }


    }
}
