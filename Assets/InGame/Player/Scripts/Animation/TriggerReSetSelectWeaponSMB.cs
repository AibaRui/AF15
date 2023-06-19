using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReSetSelectWeaponSMB : StateMachineBehaviour
{
    [Header("Off�ɂ�����Trigger�̖��O")]
    [SerializeField] private string _triggerName;

    PlayerControl _playerControl;

    /// <summary></summary>
    private int _isAttackNow;

    private void Awake()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(_triggerName);

       // _playerControl.WeaponSetting.NowChangeWeaponAnimation = false;

        //�J�E���g�����炷
        _isAttackNow--;

        if (_isAttackNow <= 0)
        {
           // _playerControl.Attack.IsAttackNow = false;
        }

      //  _playerControl.Attack.IsCanNextAttack = false;
        _playerControl.AnimControl.CaneNextAttack(false);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //�J�E���g�𑝂₷
        _isAttackNow++;
    }
}
