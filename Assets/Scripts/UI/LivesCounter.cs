using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour, ISaveSystem
{
    private int lives = 3;
    public Text livesCounter;
    public void LoadData(GameData data){ lives = data.lives; }
    public void SaveData(GameData data){ data.lives = lives; }
    public void DecrementLives(){ lives--; }
    public void IncrementLives() { lives++; }

    public void Update() 
    {
        livesCounter.text = "Lives: " + lives;
    }
}
