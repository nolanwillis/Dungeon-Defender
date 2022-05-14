using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 100;
    public int playerHealth;

    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            applyDamage(10);
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