using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : StateMachineBehaviour
{
    // Function called when animator enters animation state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If object being destroyed is a player disable
        if (animator.gameObject.CompareTag("Player"))
        {
            // Disable Input
            animator.gameObject.GetComponent<PlayerManager>().isDead = true;
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
            Lives playerLivesHandler = 
                GameObject.FindGameObjectWithTag("LivesUI").GetComponent<Lives>();
            Score playerScoreHandler =
                GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<Score>();
            //if (playerLivesHandler.playerLives > 0)
            //{
            //    playerLivesHandler.ChangeLives(-1);
            //    FindObjectOfType<LevelManager>().SpawnPlayer();
            //} 
            //else
            //{
                SceneManager.LoadScene("DeathMenu");
            //}
        } 
        // If object being destroyed is an enemy
        else
        {
            // Increase score
            GameObject.FindGameObjectWithTag("ScoreUI").
                 GetComponent<Score>().ChangeScore(100);
            //// Spawn new enemy
            //FindObjectOfType<LevelManager>().SpawnEnemy();
        }
        Destroy(animator.gameObject);
    }
}
