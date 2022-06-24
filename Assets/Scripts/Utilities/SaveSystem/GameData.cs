using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int lives;
    public int score;

    // Constructor, defines default data when there's no data to load
    public GameData()
    {
        lives = 3;
        score = 0;
    }
}
