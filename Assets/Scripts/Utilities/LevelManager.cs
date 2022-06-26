using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private SpawnPointManager spawnPointManager; 

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Set reference to spawn point manager component
        spawnPointManager = GameObject.Find("SpawnPoints").GetComponent<SpawnPointManager>();
        spawnPlayer();
    }

    // Instantiates New Player prefab
    public void spawnPlayer()
    {
        // Spawn point index player will use
        int spawnPointIndex = spawnPointManager.GetSpawnPoint();
        // Instatiate player prefab into the game
        GameObject player = Instantiate(playerPrefab, spawnPointManager.spawnPoints[spawnPointIndex].position, 
            playerPrefab.transform.rotation);
        // Reference to the camera follow component of the main camera
        CameraFollow cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        // Reference to the health bar component, of the player health bar gameObject (main UI)
        HealthBar healthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
        // Reference to the player health component of the player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        // Reference to the metadata component of the player
        Metadata metaData = player.GetComponent<Metadata>();
        // Set player health component, health bar, to health bar component
        if (playerHealth != null && healthBar != null)
        {
            playerHealth.healthBar = healthBar;
        }
        // Set camera follow component, target, to player transform
        if (cameraFollow != null && player != null)
        {
            cameraFollow.target = player.transform;
        }
        // Set Metadata component spawn point to spawn point index, generated by the spawn point manager
        if (metaData != null)
        {
            metaData.spawnPoint = spawnPointIndex;
        }
    }

    // Instantiates Enemy prefab
    public void spawnEnemy()
    {
        // Spawn point index enemy will use
        int spawnPointIndex = spawnPointManager.GetSpawnPoint();
        // Instatiate player into the game
        GameObject enemy = Instantiate(enemyPrefab, spawnPointManager.spawnPoints[spawnPointIndex].position,
            enemyPrefab.transform.rotation);
        // Reference to health bar component of the health bar gameObject (one attached to enemy prefab).
        HealthBar healthBar = enemy.transform.GetChild(0).transform.GetChild(0).gameObject
            .GetComponent<HealthBar>();
        // Reference to the player health component of the enemy
        PlayerHealth enemyHealth = enemy.GetComponent<PlayerHealth>();
        // Reference to the metadata component of the enemy
        Metadata metaData = enemy.GetComponent<Metadata>();
        // Set player health component, health bar, to health bar component
        if (enemyHealth != null && healthBar != null)
        {
            enemyHealth.healthBar = healthBar;
        }
        // Set metadata component spawn point to the spawn point index, generated by the spawn point manager
        if (metaData != null)
        {
            metaData.spawnPoint = spawnPointIndex;
        }
    }
}


