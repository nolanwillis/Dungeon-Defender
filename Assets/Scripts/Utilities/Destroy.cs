using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : StateMachineBehaviour
{
    private PlayerManager playerManager;

    // Function called when animator enters animation state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If object being destroyed is a player disable
        if (animator.gameObject.CompareTag("Player"))
        {
            // Set player input manager reference
            playerManager = animator.gameObject.GetComponent<PlayerManager>();
            // Disable input 
            playerManager.isDead = true;
        }
        // If object being destroyed is an enemy
        else if (animator.gameObject.CompareTag("Enemy"))
        {
            // Destroy capsule collider so player can walk over dying enemy
            Destroy(animator.gameObject.GetComponent<CapsuleCollider>());
            // Destroy enemy controller so enemy dosen't follow player while dying
            Destroy(animator.gameObject.GetComponent<EnemyController>());
        }
    }

    // Function called when animator exits animation state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If object being destroyed is a player
        if (animator.gameObject.CompareTag("Player"))
        {
            // Decrement lives count
            GameObject.FindGameObjectWithTag("LivesUI").
                GetComponent<LivesCounter>().DecrementLives();
        } 
        // If object being destroyed is an enemy
        else
        {
            // Increase score
            GameObject.FindGameObjectWithTag("ScoreUI").
                 GetComponent<ScoreCounter>().IncreaseScore(100);
        }
        Destroy(animator.gameObject);
    }

    
}
