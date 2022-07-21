using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    // References
    private Text livesCounter;

    // Amount of lives player has
    public int playerLives;
    
    private void Awake()
    {
        playerLives = 3;
        livesCounter = GetComponent<Text>();
    }

    // Setter method for other scripts
    public void ChangeLives(int amount)
    {
        playerLives += amount;
        livesCounter.text = "Lives: " + playerLives;
    }
}
