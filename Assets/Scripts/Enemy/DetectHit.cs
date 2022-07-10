using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : StateMachineBehaviour
{
    private EnemyController enemyController;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.gameObject.GetComponent<EnemyController>();
        enemyController.StartDelayDetectHit(0.5f);
    }
}
