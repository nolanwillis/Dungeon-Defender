using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour, ISaveSystem
{
    private int score = 0;
    public Text scoreCounter;
    public void LoadData(GameData data) { score = data.score; }
    public void SaveData(GameData data) { data.score = score; }
    public void IncreaseScore(int amount) { score += amount; }
    public void DecreaseScore(int amount) { score -= amount; }

    public void Update()
    {
        scoreCounter.text = "Score: " + score;
    }
}
