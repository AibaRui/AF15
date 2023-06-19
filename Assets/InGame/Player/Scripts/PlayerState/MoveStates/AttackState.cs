using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackState : PlayerStateBase
{
    public override void Enter()
    {
        //������o��
        //    _stateMachine.PlayerController.WeaponSetting.WeaponActives(true);

        _stateMachine.PlayerController.Attack.AttackFirst();

        _stateMachine.PlayerController.VelocityLimit.SetLimit(25, 40, 25);
    }

    public override void Exit()
    {
        _stateMachine.PlayerController.Attack.EndAttack();

        _stateMachine.PlayerController.Rb.useGravity = true;
    }

    public override void FixedUpdate()
    {
        _stateMachine.PlayerController.Attack.AttackFixedUpdata();
    }

    public override void LateUpdate()
    {

    }

    public override void Update()
    {
        //�e�N�[���^�C��
        _stateMachine.PlayerController.Allways();

        _stateMachine.PlayerController.Attack.AttackInputedCheck();
        _stateMachine.PlayerController.Attack.AttackUpdata();

        float h = _stateMachine.PlayerController.InputManager.HorizontalInput;
        float v = _stateMachine.PlayerController.InputManager.VerticalInput;

        if (_stateMachine.PlayerController.Damage.IsDamage)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDamage);
            return;
        }   //�_���[�W���󂯂�

        //�X�e�b�v
        //�X�e�b�v���������Ă���A�X�e�b�v�\�B�U�����I������A�U���{�^���������ꂽ�A�ړ����͂�����B�U������������
        //����̃^�C�v�������A��
        if (_stateMachine.PlayerController.Step.IsCanStep && _stateMachine.PlayerController.Step.IsCanStep
            && _stateMachine.PlayerController.Attack.IsCanNextAttack && _stateMachine.PlayerController.Attack.IsPushAttack
            && (h != 0 || v != 0) && _stateMachine.PlayerController.AllWeapons.AttackRange.IsHitAttack && !_stateMachine.PlayerController.GroundCheck.IsHit()
            && (_stateMachine.PlayerController.WeaponSetting.NowWeapon.WeaponType == AllWeapons.WeaponType.Sword ||
            _stateMachine.PlayerController.WeaponSetting.NowWeapon.WeaponType == AllWeapons.WeaponType.Spear)
            && _stateMachine.PlayerController._stepInfo.IsCanStep && (_stateMachine.PlayerController.Step.StepCount<_stateMachine.PlayerController._stepInfo.StepLevel))

        {
            //��������܂�
            _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

            _stateMachine.TransitionTo(_stateMachine.StepState);
            return;
        }

        ///���
        if (_stateMachine.PlayerController.Avoid.IsRoalInput && _stateMachine.PlayerController.Avoid.IsCanAvoid && !_stateMachine.PlayerController.Avoid.IsCanNotAvoid && !_stateMachine.PlayerController.Attack.IsAttackNow)
        {
            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //�U���񐔂����Z�b�g
                _stateMachine.PlayerController.Attack.ReSetAttackCount();
            }

            //��������܂�
            _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

            _stateMachine.PlayerController.Avoid.SetAvoidDir();
            _stateMachine.TransitionTo(_stateMachine.AvoidState);
            return;
        }

        int attackCount = _stateMachine.PlayerController.AllWeapons.WeaponGetActionData(_stateMachine.PlayerController.WeaponSetting.NowWeapon.WeaponType).AttackCount;

        if (_stateMachine.PlayerController.Attack.IsCanNextAttack && _stateMachine.PlayerController.Attack.IsPushAttack && _stateMachine.PlayerController.Attack.IsCanAttack)
        {
            if (attackCount > 3 && !_stateMachine.PlayerController.GroundCheck.IsHit()) return;

            ////////////////////////////////_stateMachine.PlayerController.WeaponSetting.CheckWeapon();

            _stateMachine.TransitionTo(_stateMachine.AttackState);

            return;
        }



        if (!_stateMachine.PlayerController.Attack.IsAttackNow)
        {
            //////////////_stateMachine.PlayerController.WeaponSetting.CheckWeapon();

            if (_stateMachine.PlayerController.GroundCheck.IsHit())
            {
                //�U���񐔂����Z�b�g
                _stateMachine.PlayerController.Attack.ReSetAttackCount();

                //��������܂�
                _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

                _stateMachine.TransitionTo(_stateMachine.StateIdle);
            }
            else
            {
                if (_stateMachine.PlayerController.Rb.velocity.y > 0)
                {
                    //��������܂�
                    _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

                    _stateMachine.TransitionTo(_stateMachine.StateUpAir);
                }      //�㏸
                else
                {
                    //��������܂�
                    _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

                    _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                }   //�~��

            }
        }

    }
}
