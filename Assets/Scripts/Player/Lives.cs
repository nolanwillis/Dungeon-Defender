using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public int playerLives = 3;
    private Text livesCounter;

    private void Awake()
    {
        livesCounter = GetComponent<Text>();
    }

    public void ChangeLives(int amount)
    {
        playerLives += amount;
        livesCounter.text = "Lives: " + playerLives;
    }
}
