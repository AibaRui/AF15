using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StepState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.SoundManager.PlaySound(AudioType.Step);
        //使っていた武器の値をリセット
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
        //各クールタイム
        _stateMachine.PlayerController.Allways();

        //攻撃ボタンを押したかどうかの確認
        _stateMachine.PlayerController.Attack.AttackInputedCheck();

        //移動し終えたら
        if (_stateMachine.PlayerController.Step.CheckMoveDistance())
        {
            //攻撃に移行
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
                }      //上昇
                else
                {
                    _stateMachine.TransitionTo(_stateMachine.StateDownAir);
                }   //降下

            }

        }
    }
}
