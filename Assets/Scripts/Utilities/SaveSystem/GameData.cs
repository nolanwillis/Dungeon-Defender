using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int highScore;

    // Constructor, defines default data when there's no data to load
    public GameData()
    {
        highScore = 0;
    }
}
