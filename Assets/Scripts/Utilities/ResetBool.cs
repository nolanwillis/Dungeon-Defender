using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetBool : StateMachineBehaviour
{
    [Header("Enter Booleans")]
    [SerializeField] private string enterParam1;
    [SerializeField] private bool enterParam1Status;

    [Header("Exit Booleans")]
    [SerializeField] private string exitParam1;
    [SerializeField] private bool exitParam1Status;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(enterParam1, enterParam1Status);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(exitParam1, exitParam1Status);
    }
}
