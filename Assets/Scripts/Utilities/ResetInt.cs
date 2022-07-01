using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInt : StateMachineBehaviour
{
    public string intParam;
    public int  intParamValue;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(intParam, intParamValue);
    }
}
