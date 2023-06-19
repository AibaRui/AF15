using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReSet : StateMachineBehaviour
{
    [SerializeField] string triggerName;

    [SerializeField] private PlayerControl _o;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(triggerName);
    }

}
