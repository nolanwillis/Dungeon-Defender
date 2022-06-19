using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Max health player can have
    [SerializeField] private int playerMaxHealth = 100;
    // Current player health
    private int playerHealth;
    // Reference to Health Bar component of the HealthBar GameObject
    public HealthBar healthBar;


    private void Start()
    {
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
            if (gameObject.tag == "Player")
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

    public void applyDamage(int amount)
    {
        if (playerHealth - amount >= 0)
        {
            playerHealth -= amount;
        }
        else
        {
            playerHealth = 0;
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