using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // References
    public HealthBar healthBar;
    private Animator animatorController;
    
    // Max health player can have
    [SerializeField] private int playerMaxHealth = 100;
    // Current player health
    private int playerHealth;

    // Animator hashes
    int isBlockingHash = Animator.StringToHash("isBlocking");

    private void Awake()
    {
        // Set references
        animatorController = GetComponent<Animator>();
        // Set current health to max health
        playerHealth = playerMaxHealth;
        // Set max health of player
        healthBar.SetMaxHealth(playerMaxHealth);

    }

    public void applyDamage(int amount)
    {
        if (playerHealth - amount >= 0)
        {
            playerHealth -= amount;
            if (gameObject.CompareTag("Player")){
                if (animatorController.GetBool(isBlockingHash))
                {
                    animatorController.CrossFade("blockedImpact", 0.1f);
                }
                else
                {
                    animatorController.CrossFade("unblockedImpact", 0.1f);
                }
            }
            // If not player, play impact animation for enemy
        }
        else
        {
            playerHealth = 0;
            animatorController.CrossFade("death", 0.1f);
            StartCoroutine(DelayDestroy(1.0f));
        }
        healthBar.setHealth(playerHealth);
    }

    public void addHealth(int amount)
    {
        if (playerHealth + amount <= playerMaxHealth)
        {
            playerHealth += amount;
        }
        else
        {
            playerHealth = playerMaxHealth;
            healthBar.setHealth(playerHealth);
        } 
    }

    IEnumerator DelayDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
        if (gameObject.CompareTag("Player"))
        {
            // Spawn new player
            LevelManager.instance.spawnPlayer();
            // Decrement lives count
            GameObject.FindGameObjectWithTag("LivesUI").
                GetComponent<LivesCounter>().DecrementLives();
        }
        else
        {
            // If tag != "Player" then PlayerHealth component must be
            // attached to enemy. So increase player score by 100.
            GameObject.FindGameObjectWithTag("ScoreUI").
                 GetComponent<ScoreCounter>().IncreaseScore(100);
        }
    }
}