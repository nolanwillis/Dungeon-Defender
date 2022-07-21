using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInt : StateMachineBehaviour
{
    // Animation parameter information
    [SerializeField] private string intParam;
    [SerializeField] private int  intParamValue;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(intParam, intParamValue);
    }
}
