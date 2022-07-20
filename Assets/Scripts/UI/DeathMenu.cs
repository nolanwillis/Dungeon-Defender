using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        GameObject.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text 
            = "Score: " + Score.score.ToString();
        GameObject.Find("HighScore").GetComponent<TMPro.TextMeshProUGUI>().text
            = "High Score: " + Score.highScore.ToString();
    }
}
