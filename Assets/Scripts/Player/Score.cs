using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour, ISaveSystem
{
    // References
    private Text scoreCounter;
    
    public int highScore = 0;
    public int score = 0;

    private void Awake()
    {
        scoreCounter = GetComponent<Text>();
    }
    
    // Called by other scripts to change score
    public void ChangeScore(int amount)
    {
        score += amount;
        print(score);
        scoreCounter.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
        }
    }

    // Save system functions
    public void LoadData(GameData data) { score = data.highScore; }
    public void SaveData(GameData data) { data.highScore = score; }
}
