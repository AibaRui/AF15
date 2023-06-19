using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIK : MonoBehaviour
{
    public Transform playerHand;
    public Transform weapon;

    private Animator animator;
    private Vector3 weaponOriginalPosition;
    private Quaternion weaponOriginalRotation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        weaponOriginalPosition = weapon.localPosition;
        weaponOriginalRotation = weapon.localRotation;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator != null)
        {
            // プレイヤーの手の位置と回転を指定
            animator.SetIKPosition(AvatarIKGoal.RightHand, playerHand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, playerHand.rotation);

            // プレイヤーの手に武器を持っていく
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
    }

    public void ResetWeaponPosition()
    {
        weapon.localPosition = weaponOriginalPosition;
        weapon.localRotation = weaponOriginalRotation;
    }
}
