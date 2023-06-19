using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeaponSelectState : PlayerStateBase
{
    public override void Enter()
    {
        //����̕ύX�B����Ɏ����Ă����\���ɂ��āA���̕�����Z�b�g
        _stateMachine.PlayerController.WeaponSetting.CheckWeaponOnSelect();

        //�A�j���[�V�������Đ�
        _stateMachine.PlayerController.WeaponSetting.WeaponChangeGroundAnim();

        _stateMachine.PlayerController.AllWeapons.WeaponGetActionData(_stateMachine.PlayerController.WeaponSetting.NowWeapon.WeaponType).SelectWeapon();
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

        if (_stateMachine.PlayerController.Damage.IsDamage)
        {
            _stateMachine.TransitionTo(_stateMachine.StateDamage);
            return;
        }   //�_���[�W���󂯂�

        ///���
        if (_stateMachine.PlayerController.InputManager.IsInputDownAvoidButttun && _stateMachine.PlayerController.Avoid.IsCanAvoid
            && !_stateMachine.PlayerController.Avoid.IsCanNotAvoid)
        {
            //������\��
            _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

            _stateMachine.PlayerController.Avoid.SetAvoidDir();

            _stateMachine.TransitionTo(_stateMachine.AvoidState);
            return;
        }

            //����`�F���W
            if (_stateMachine.PlayerController.WeaponSetting.IsWeaponChangeAnimation)
        {
            Debug.Log("ChangeWeapon");
            _stateMachine.TransitionTo(_stateMachine.WeaponSelectState);
            return;
        }

        //�U��
        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            Debug.Log("Attack");
            _stateMachine.TransitionTo(_stateMachine.AttackState);
            return;
        }

        //������o���Ă��܂��܂ł̃A�j���[�V�������I�������߂�
        if (!_stateMachine.PlayerController.WeaponSetting.NowChangeWeaponAnimation)
        {
            Debug.Log("End");

            _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

            _stateMachine.TransitionTo(_stateMachine.StateIdle);
        }
    }
}
