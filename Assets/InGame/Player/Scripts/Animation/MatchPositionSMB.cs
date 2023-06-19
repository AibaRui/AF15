using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MatchPositionSMB : StateMachineBehaviour
{
    //[Header("Match Settings")]
    //[SerializeField] private AvatarTarget _targetBodyPart = AvatarTarget.Root;
    ////[SerializeField,MinMaxSlider(0,1)] private Vector2 _effectiveRange;


    //[Header("Att Settings")]
    //[SerializeField, Range(0, 1)] private float _assistPower = 1;
    //[SerializeField, Range(0, 10)] private float _assistDistance;

    //private IMatchTarget _target;

    //private MatchTargetWeightMask _weightMask;
    //private bool _isSkip = false;
    //private bool _isInitialized = false;

    //public IMatchTarget MatchTarget { get => _target; set => _target = value; }

    //public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if(_isInitialized == false)
    //    {
    //        var weight = new Vector3(_assistPower, 0, _assistPower);
    //        _weightMask = new MatchTargetWeightMask(weight, 0);
    //        _isInitialized = true;
    //    }

    //    _isSkip = Vector3.Distance(_target.TargetPosition(), animator.rootPosition) > _assistDistance;
    //}

    //public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if(_isSkip == true || animator.IsInTransition(layerIndex))
    //    {
    //        return;
    //    }

    //    if(stateInfo.normalizedTime>_effectiveRange.y)
    //    {
    //        animator.InterruptMatchTarget(false);
    //    }
    //    else
    //    {
    //        animator.MatchTarget(_target.TargetPosition(),
    //            animator.bodyRotation,
    //            _targetBodyPart,
    //            _weightMask,
    //            _effectiveRange.x, _effectiveRange.y);
    //    }
    //}
}
