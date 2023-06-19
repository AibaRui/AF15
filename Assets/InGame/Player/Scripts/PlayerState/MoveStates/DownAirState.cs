using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DownAirState : PlayerStateBase
{
    public override void Enter()
    {
        //�J�������������ɂ���
        // _stateMachine.PlayerController.CameraControl.UseSwingCamera();

        //���x�ݒ�
        _stateMachine.PlayerController.VelocityLimit.SetLimit(25, 40, 25);
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
        ////�J�����̎���
        //_stateMachine.PlayerController.CameraControl.CountTime();
        ////�J�����̌X����߂�
        //_stateMachine.PlayerController.CameraControl.AirCameraYValue(_stateMachine.PlayerController.Rb.velocity.y);

        ////�J�������v���C���[�̌��Ɏ����I�Ɍ�����BX��
        //_stateMachine.PlayerController.CameraControl.SwingEndCameraAutoFollow();

        ////�J�������X����BX��
        //_stateMachine.PlayerController.CameraControl.SwingCameraValueX(false);
    }

    public override void Update()
    {
        //�e����̃N�[���^�C��
        _stateMachine.PlayerController.Allways();

        if (_stateMachine.PlayerController.Damage.IsDamage)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDamage);
            return;
        }   //�_���[�W���󂯂�

        if (_stateMachine.PlayerController.InputManager.IsShiftBreak && _stateMachine.PlayerController.ShiftBreak.IsCanShiftBreak)
        {
            _stateMachine.TransitionTo(_stateMachine.ShiftBreakState);
            return;
        }   //�V�t�g�u���C�N

        //�U��
        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            _stateMachine.TransitionTo(_stateMachine.AttackState);
            return;
        }

        if (_stateMachine.PlayerController.GroundCheck.IsHit())
        {
            _stateMachine.PlayerController.SoundManager.PlaySound(AudioType.Land);
            _stateMachine.TransitionTo(_stateMachine.StateIdle);

            return;
        }   //�n��


        //����������Ă��鎞�Ɉڍs�\
        if (_stateMachine.PlayerController.HoldAvoidMove.IsHoldAvoid)
        {
            _stateMachine.TransitionTo(_stateMachine.StateholdAvoidAir);
            return;
        }


        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            if (_stateMachine.PlayerController.Attack.IsCanAttack)
            {
                _stateMachine.TransitionTo(_stateMachine.AttackState);
            }
        }   //�U���X�e�[�g
    }
}
