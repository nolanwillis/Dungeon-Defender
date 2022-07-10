using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // References
    public HealthBar healthBar;
    private Animator animatorController;
    
    // Max player health
    [SerializeField] private int playerMaxHealth = 100;
    // Current player health
    private int playerHealth;

    private void Start()
    {
        // Set references
        animatorController = GetComponent<Animator>();
        // Set current health to max health
        playerHealth = playerMaxHealth;
        // Set max health of player
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    public void ApplyDamage(int amount)
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
        healthBar.setHealth(playerHealth);
    }

    public void AddHealth(int amount)
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