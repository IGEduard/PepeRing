using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    public string isUsingRightHand = "isUsingRightHand";
    public bool isUsingRightHandStatus = false;

    public string isUsingLeftHand = "isUsingLeftHand";
    public bool isUsingLeftHandStatus = false;

    public string isInvulnerable = "isInvulnerable";
    public bool isInvulnerableStatus = false;

    public string isInteractingBool = "isInteracting";
    public bool isInteractingStatus = false;

    public string isFiringSpellBool = "isFiringSpell";
    public bool isFiringSpellStatus = false;

    public string canRotateBool = "canRotate";
    public bool canRotateStatus = true;

    public string isRotatingWithRootMotion = "isRotatingWithRootMotion";
    public bool isRotatingWithRootMotionStatus = false;

    //public string targetBool;
    //public bool status;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.SetBool(isInteractingBool, isInteractingStatus);
        animator.SetBool(isFiringSpellBool, isFiringSpellStatus);
        animator.SetBool(isRotatingWithRootMotion, isRotatingWithRootMotionStatus);
        animator.SetBool(canRotateBool, canRotateStatus);
        animator.SetBool(isInvulnerable,isInvulnerableStatus);
        animator.SetBool(isUsingLeftHand, isUsingLeftHandStatus);
        animator.SetBool(isUsingRightHand, isUsingRightHandStatus);
    }
}
