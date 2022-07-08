using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // References
    public HealthBar healthBar;
    private Animator animatorController;
    private AudioManager audioManager;
    
    // Max player health
    [SerializeField] private int playerMaxHealth = 100;
    // Current player health
    private int playerHealth;

    // Animator hashes
    int isBlockingHash = Animator.StringToHash("isBlocking");

    private void Start()
    {
        // Set references
        animatorController = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        // Set current health to max health
        playerHealth = playerMaxHealth;
        // Set max health of player
        healthBar.SetMaxHealth(playerMaxHealth);

    }

    public void applyDamage(int amount)
    {
        // Apply damage differently if entity being damaged is the player
        if (gameObject.CompareTag("Player"))
        {
            // If player is currently blocking
            if (animatorController.GetBool(isBlockingHash))
            {
                animatorController.CrossFade("blockedImpact", 0.1f);
            }
            else
            {
                if (playerHealth - amount > 0)
                {
                    playerHealth -= amount;
                }
                else
                {
                    // Set health to 0
                    playerHealth = 0;
                    // Play death animation
                    animatorController.CrossFade("death", 0.1f);
                }
            }
        }
        else
        {
            if (playerHealth - amount > 0)
            {
                playerHealth -= amount;
            }
            else
            {
                // Set health to 0
                playerHealth = 0;
                // Play death animation
                animatorController.CrossFade("death", 0.1f);
            }
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

}