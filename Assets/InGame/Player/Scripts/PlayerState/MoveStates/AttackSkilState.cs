using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AttackSkilState : PlayerStateBase
{
    public override void Enter()
    {
        _stateMachine.PlayerController.AttackSkill.UseSkill();
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
       
    }
}
