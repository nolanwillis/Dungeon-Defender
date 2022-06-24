using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameDataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    // Name of data file
    [SerializeField] private string fileName;
    // Encryption toggle
    [SerializeField] private bool useEncryption;
    // Reference to gameData object
    private GameData gameData;
    // Reference to LivesCounter gameObject
    public LivesCounter livesCounter;
    // Reference to ScoreCounter gameObject;
    public ScoreCounter scoreCounter;
    private List<ISaveSystem> saveSystemObjects;
    
    // Reference to DataFileHandler script 
    private DataFileHandler dataHandler;
    public static GameDataManager instance { get; private set; }
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Game Data Manager found!");
        }
        instance = this;
    }

    private void Start()
    {
        dataHandler = new DataFileHandler(Application.persistentDataPath, fileName,
            useEncryption);
        //saveSystemObjects = FindAllSaveSystemObjects();
        LoadGame();
    }

    // NOT WORKING: saveSystemObjects should be of length 1 not 0
    private List<ISaveSystem> FindAllSaveSystemObjects()
    {
        // Create a list of all scripts that implement ISaveSystem and extend
        // MonoBehavior.
        IEnumerable<ISaveSystem> saveSystemObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<ISaveSystem>();
        
        return new List<ISaveSystem>(saveSystemObjects);
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
        //print("saveSystemObjects List Length: " + saveSystemObjects.Count);
        //// Load data in each script that implements ISaveSystem
        //foreach (ISaveSystem saveSystemObj in saveSystemObjects)
        //{
        //    saveSystemObj.LoadData(gameData);
        //}
        livesCounter.GetComponent<LivesCounter>().LoadData(gameData);
        scoreCounter.GetComponent<ScoreCounter>().LoadData(gameData);
    }

    public void SaveGame()
    {
        //// Save data in each script that implements ISaveSystem
        //foreach (ISaveSystem saveSystemObj in saveSystemObjects)
        //{
        //    saveSystemObj.SaveData(gameData);
        //}
        livesCounter.GetComponent<LivesCounter>().SaveData(gameData);
        scoreCounter.GetComponent <ScoreCounter>().SaveData(gameData);
        // Save data to a file using data file handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
