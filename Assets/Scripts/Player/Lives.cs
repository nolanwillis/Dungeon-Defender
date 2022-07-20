using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public int playerLives;
    private Text livesCounter;

    private void Awake()
    {
        playerLives = 3;
        livesCounter = GetComponent<Text>();
    }

    public void ChangeLives(int amount)
    {
        playerLives += amount;
        livesCounter.text = "Lives: " + playerLives;
    }
}
