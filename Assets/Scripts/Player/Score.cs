using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour, ISaveSystem
{
    // References
    private Text scoreCounter;
    
    // Must be public static so data can be accessed across scenes
    public static int highScore = 0;
    public static int score;

    private void Awake()
    {
        score = 0;
        scoreCounter = GetComponent<Text>();
    }
    
    // Called by other scripts to change score
    public void ChangeScore(int amount)
    {
        score += amount;
        scoreCounter.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
        }
    }

    // Save system functions
    public void LoadData(GameData data) { highScore = data.highScore; }
    public void SaveData(GameData data) { data.highScore = highScore; }
}
