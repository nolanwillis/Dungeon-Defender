using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Max health player can have
    public int playerMaxHealth = 100;
    // Current player health
    public int playerHealth;
    // Reference to Health Bar component of the HealthBar GameObject
    public HealthBar healthBar;
    private void Start()
    {
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerHealth>().playerHealth <= 0)
        {
            Destroy(gameObject);
            if (gameObject.tag == "Player")
            {
                LevelManager.instance.spawnPlayer();
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
        }
    }

    
}