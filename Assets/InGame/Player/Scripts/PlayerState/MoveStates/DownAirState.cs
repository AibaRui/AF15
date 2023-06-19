using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DownAirState : PlayerStateBase
{
    public override void Enter()
    {
        //カメラを遠巻きにする
        // _stateMachine.PlayerController.CameraControl.UseSwingCamera();

        //速度設定
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
        ////カメラの時間
        //_stateMachine.PlayerController.CameraControl.CountTime();
        ////カメラの傾きを戻す
        //_stateMachine.PlayerController.CameraControl.AirCameraYValue(_stateMachine.PlayerController.Rb.velocity.y);

        ////カメラをプレイヤーの後ろに自動的に向ける。X軸
        //_stateMachine.PlayerController.CameraControl.SwingEndCameraAutoFollow();

        ////カメラを傾ける。X軸
        //_stateMachine.PlayerController.CameraControl.SwingCameraValueX(false);
    }

    public override void Update()
    {
        //各動作のクールタイム
        _stateMachine.PlayerController.Allways();

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

        //攻撃
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
        }   //地面


        //回避を押している時に移行可能
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
        }   //攻撃ステート
    }
}
