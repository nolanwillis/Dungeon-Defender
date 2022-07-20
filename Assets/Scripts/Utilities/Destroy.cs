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
        // Set reference to player lives handler
        Lives playerLivesHandler =
            GameObject.FindGameObjectWithTag("LivesUI").GetComponent<Lives>();
        // Set reference to 
        SpawnPointManager spawnPointManager =
            GameObject.Find("SpawnPoints").GetComponent<SpawnPointManager>();
        // If object being destroyed is a player
        if (animator.gameObject.CompareTag("Player"))
        {
            if (playerLivesHandler.playerLives > 0)
            {
                // Remove lives
                playerLivesHandler.ChangeLives(-1);
                // Reset spawn point
                spawnPointManager.OpenSpawnPoint(
                    animator.gameObject.GetComponent<Metadata>().spawnPoint);
                // Increment spawn point counter. Everytime spawn player or enemy
                // is called the spawn point counter is decremented by one in the
                // spawn point manager getSpawnPoint method. In this case we're
                // respawning not spawning a new object, so the total number of 
                // open spawns should be the same (bad design, I know). 
                spawnPointManager.numOpenSpawns++;
                // Spawn player
                FindObjectOfType<LevelManager>().SpawnPlayer();
            }
            else
            {
                // Reset spawn point
                spawnPointManager.OpenSpawnPoint(
                    animator.gameObject.GetComponent<Metadata>().spawnPoint);
                // Save data
                GameObject.Find("GameDataManager").GetComponent<GameDataManager>().SaveGame();                
                // Load death menu scene
                SceneManager.LoadScene("DeathMenu");
            }
        } 
        // If object being destroyed is an enemy
        else
        {
            // Increase score
            GameObject.FindGameObjectWithTag("ScoreUI").
                 GetComponent<Score>().ChangeScore(100);
            // Reset spawn point
            spawnPointManager.OpenSpawnPoint(
                    animator.gameObject.GetComponent<Metadata>().spawnPoint);
            // Increment spawn point counter;
            spawnPointManager.numOpenSpawns++;
            // Spawn new enemy
            FindObjectOfType<LevelManager>().SpawnEnemy();
        }
        Destroy(animator.gameObject);
    }
}
