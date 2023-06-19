//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public class PlayerAnimationControl
//{
//    [SerializeField] private Transform _reftHandPos;

//    [SerializeField] private Transform _rightHandPos;

//    [SerializeField]
//    TriggerReSet _t;

//    [Header("�X�e�b�v")]
//    [SerializeField] private string _isStep;

//    [Header("���")]
//    [SerializeField] private string _isAvoid;

//    [Header("�A�V�X�g")]
//    [SerializeField] private string _isAssist;

//    [SerializeField] private string _isAttack = "IsAttack";

//    [Header("�U���g���K�[")]
//    [SerializeField] private string _attackTrriger;

//    [Header("�U����")]
//    [SerializeField] private string _attackCount;

//    [Header("���ɍU���\")]
//    [SerializeField] private string _isCanNextAttac;

//    [Header("�W�����v")]
//    [SerializeField] private string _jump;

//    private PlayerControl _playerControl;
//    public PlayerControl PlayerControl => _playerControl;

//    public Transform ModelLeftHandPos => _reftHandPos;
//    public Transform ModelRightHandPos => _rightHandPos;
//    public void Init(PlayerControl playerControl)
//    {
//        _playerControl = playerControl;
//    }


//    public void AnimSet()
//    {
//        _playerControl.Animator.SetFloat("Speed", _playerControl.Rb.velocity.magnitude);
//        _playerControl.Animator.SetFloat("SpeedY", _playerControl.Rb.velocity.y);
//        _playerControl.Animator.SetBool("IsGround", _playerControl.GroundCheck.IsHit());
//        _playerControl.Animator.SetBool("IsHoldAvoid", _playerControl.HoldAvoidMove.IsHoldAvoid);
//        _playerControl.Animator.SetBool(_isAttack, _playerControl.Attack.IsAttackNow);
//    }
    
//    public void Jump()
//    {
//        _playerControl.Animator.Play(_jump);
//    }

//    public void StepSet(bool isOn)
//    {
//        _playerControl.Animator.SetBool(_isStep, isOn);
//    }


//    public void AvoidSet(bool isOn)
//    {
//        _playerControl.Animator.SetBool(_isAvoid, isOn);
//    }

//    public void AssistSet(bool isOn)
//    {
//        _playerControl.Animator.SetBool(_isAssist, isOn);
//    }

//    public void PlayerAnimPlay(string name)
//    {
//        _playerControl.Animator.Play(name);
//    }

//    public void AttackAnim()
//    {
//        _playerControl.Animator.SetBool(_isAttack, false);
//        _playerControl.Animator.SetTrigger(_attackTrriger);
//    }


//    public void SetAttackCount(int thisWeaponAttackCount)
//    {
//        _playerControl.Animator.SetInteger(_attackCount, thisWeaponAttackCount);
//    }

//    public void CaneNextAttack(bool isOn)
//    {
//        _playerControl.Animator.SetBool(_isAttack, true);
//        _playerControl.Animator.SetBool(_isCanNextAttac, isOn);

//    }


//}
