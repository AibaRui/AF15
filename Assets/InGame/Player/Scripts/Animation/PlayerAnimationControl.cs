using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimationControl
{
    [Header("右手")]
    [SerializeField] private Transform _modelRightHand;

    [Header("左手")]
    [SerializeField] private Transform _modelLeftHand;

    [Header("StepのBoolのパラメータ名")]
    [SerializeField] private string _stepBoolParameta = "IsStep";

    [Header("アシストのBoolのパラメータ名")]
    [SerializeField] private string _assistBoolParameta = "IsAssist";

    [Header("回避構えのBoolのパラメータ名")]
    [SerializeField] private string _holdAvoidBoolParameta = "IsHoldAvoid";

    [Header("回避のBoolのパラメータ名")]
    [SerializeField] private string _avoidBoolParameta = "IsAvoid";

    public Transform ModelRightHandPos => _modelRightHand;

    public Transform ModelLeftHandPos => _modelLeftHand;

    private PlayerControl _playerControl;

    public enum AnimType
    {
        ShiftBreakStart,
        ShiftBreakEnd,
        Step,
    }

    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    public void AnimSet()
    {
        _playerControl.Animator.SetFloat("Speed", _playerControl.Rb.velocity.magnitude);
        _playerControl.Animator.SetFloat("SpeedY", _playerControl.Rb.velocity.y);
        _playerControl.Animator.SetBool("IsGround", _playerControl.GroundCheck.IsHit());
        _playerControl.Animator.SetFloat("PosY", _playerControl.PlayerT.position.y);
        _playerControl.Animator.SetBool(_holdAvoidBoolParameta, _playerControl.HoldAvoidMove.IsHoldAvoid);
    }


    public void Avoid(string name)
    {
        _playerControl.Animator.Play(name);
    }

    public void AttackAnim()
    {
        _playerControl.Animator.SetTrigger("Attack");
    }

    public void FrontZip()
    {

        _playerControl.Animator.SetTrigger("FrontZip");
    }


    public void AvoidSet(bool isAvoid)
    {
        _playerControl.Animator.SetBool(_avoidBoolParameta, isAvoid);
    }
    public void WallRunSet(bool isHit)
    {
        _playerControl.Animator.SetBool("IsWallHit", isHit);
    }

    public void StepSet(bool isStep)
    {
        _playerControl.Animator.SetBool(_stepBoolParameta, isStep);
    }

    public void AssistSet(bool isAssist)
    {
        _playerControl.Animator.SetBool(_assistBoolParameta, isAssist);
    }

    public void CaneNextAttack(bool isAtack)
    {
        _playerControl.Animator.SetBool("IsAttack", isAtack);
    }

    public void SetAttackCount(int num)
    {
        _playerControl.Animator.SetInteger("AttackCount", num);
    }

    public void PlayerAnimPlay(string animName)
    {
        _playerControl.Animator.Play(animName);
    }



    public void WallRunUpSet(bool judge)
    {
        _playerControl.Animator.SetBool("IsWallRunUp", judge);
    }



    public void Jump()
    {
        _playerControl.Animator.Play("JumpStart");
    }
}
