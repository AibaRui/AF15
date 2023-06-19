using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeaponSelectState : PlayerStateBase
{
    public override void Enter()
    {
        //武器の変更。今手に持ってるやつを非表示にして、次の武器をセット
        _stateMachine.PlayerController.WeaponSetting.CheckWeaponOnSelect();

        //アニメーションを再生
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
        }   //ダメージを受けた

        ///回避
        if (_stateMachine.PlayerController.InputManager.IsInputDownAvoidButttun && _stateMachine.PlayerController.Avoid.IsCanAvoid
            && !_stateMachine.PlayerController.Avoid.IsCanNotAvoid)
        {
            //武器を非表示
            _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

            _stateMachine.PlayerController.Avoid.SetAvoidDir();

            _stateMachine.TransitionTo(_stateMachine.AvoidState);
            return;
        }

            //武器チェンジ
            if (_stateMachine.PlayerController.WeaponSetting.IsWeaponChangeAnimation)
        {
            Debug.Log("ChangeWeapon");
            _stateMachine.TransitionTo(_stateMachine.WeaponSelectState);
            return;
        }

        //攻撃
        if (_stateMachine.PlayerController.InputManager.IsAttack)
        {
            Debug.Log("Attack");
            _stateMachine.TransitionTo(_stateMachine.AttackState);
            return;
        }

        //武器を出してしまうまでのアニメーションが終わったら戻る
        if (!_stateMachine.PlayerController.WeaponSetting.NowChangeWeaponAnimation)
        {
            Debug.Log("End");

            _stateMachine.PlayerController.WeaponSetting.WeaponActives(false);

            _stateMachine.TransitionTo(_stateMachine.StateIdle);
        }
    }
}
