using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : StateMachineBehaviour
{
    // References
    private EnemyController enemyController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.gameObject.GetComponent<EnemyController>();
        enemyController.StartDelayDetectHit(0.5f);
    }
}
