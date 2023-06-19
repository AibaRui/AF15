using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StepState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.SoundManager.PlaySound(AudioType.Step);
        //�g���Ă�������̒l�����Z�b�g
        _stateMachine.PlayerController.AllWeapons.WeaponGetActionData(_stateMachine.PlayerController.WeaponSetting.NowWeapon.WeaponType).ReSetValue();

        _stateMachine.PlayerController.Step.StepStartSetting();

        _stateMachine.PlayerController.Attack.IsCanAttack = true;
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.Step.StepFixedUpdata();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        //�e�N�[���^�C��
        _stateMachine.PlayerController.Allways();

        //�U���{�^�������������ǂ����̊m�F
        _stateMachine.PlayerController.Attack.AttackInputedCheck();

        //�ړ����I������
        if (_stateMachine.PlayerController.Step.CheckMoveDistance())
        {
            //�U���Ɉڍs
            if (_stateMachine.PlayerController.Attack.IsPushAttack && _stateMachine.PlayerController.Attack.IsCanAttack)
            {
               ///// _stateMachine.PlayerController.WeaponSetting.CheckWeapon();

               _stateMachine.TransitionTo(_stateMachine.AttackState);

                return;
            }

            if (!_stateMachine.PlayerController.GroundCheck.IsHit())
            {

              //////  _stateMachine.PlayerController.WeaponSetting.CheckWeapon();

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
