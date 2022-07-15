using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameDataManager : MonoBehaviour
{
    // Make class a single instance
    public static GameDataManager instance { get; private set; }
    
    // References
    private GameData gameData;
    private DataFileHandler dataHandler;
    private Score playerScoreHandler;

    [Header("File Storage Config")]
    // Name of data file
    [SerializeField] private string fileName;
    // Encryption toggle
    [SerializeField] private bool useEncryption;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Game Data Manager found!");
        }
        instance = this;

        playerScoreHandler = GameObject.FindGameObjectWithTag("ScoreUI")
            .GetComponent<Score>();
    }

    private void Start()
    {
        dataHandler = new DataFileHandler(Application.persistentDataPath, fileName,
            useEncryption);
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load any saved data from a file using data file handler
        gameData = dataHandler.Load();
        // If there's no data create new game
        if (gameData == null)
        {
            Debug.Log("No game to load. Creating new game...");
            NewGame();
        }

        // Call load data in all scripts that load data
        playerScoreHandler.LoadData(gameData);
    }

    public void SaveGame()
    {
        // Call save method in all scripts that save data
        playerScoreHandler.SaveData(gameData);

        // Save data to a file using data file handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
