using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    // Animation parameter information
    [SerializeField] private string enterParam;
    [SerializeField] private bool enterParamStatus;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(enterParam, enterParamStatus);
    }
}
