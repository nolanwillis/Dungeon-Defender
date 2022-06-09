using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // Reference to player gameObject
    public GameObject playerRef;
    // Reference to player prefab
    public GameObject playerPrefab;
    // Reference to enemy prefab
    public GameObject enemyPrefab;
    // Reference to SpawnPointManager component
    private SpawnPointManager spm; 

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Set reference to SpawnPointManager component
        spm = GameObject.Find("SpawnPoints").GetComponent<SpawnPointManager>();
        spawnPlayer();
        spawnEnemy();
    }

    // spawnPlayer, instantiates New Player prefab
    public void spawnPlayer()
    {
        // Index in SPM's spawnPoints array this prefab will use
        int spawnPointIndex = spm.GetSpawnPoint();
        // Instatiate player prefab into the game
        GameObject player = Instantiate(playerPrefab, spm.spawnPoints[spawnPointIndex].position, 
            playerPrefab.transform.rotation);
        // Set reference to player gameObject for future use
        playerRef = player;
        // Reference to the Camera Follow component of the Main Camera
        CameraFollow cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        // Reference to the Health Bar component of the Player Health Bar (the big one)
        HealthBar hb = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
        // Reference to the Player Health component of the player
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        // Reference to the Metadata component of the player
        Metadata md = player.GetComponent<Metadata>();
        // Set Player Health component Health Bar to healthbar gameobject
        if (ph != null && hb != null)
        {
            ph.healthBar = hb;
        }
        // Set Camera Follow component Target to player transform
        if (cf != null && player != null)
        {
            cf.target = player.transform;
        }
        // Set Metadata component spawn point to spawnPointIndex
        if (md != null)
        {
            md.spawnPoint = spawnPointIndex;
        }
    }

    // spawnEnemy, instantiates Enemy prefab
    public void spawnEnemy()
    {
        // Index in SPM's spawnPoints array this prefab will use
        int spawnPointIndex = spm.GetSpawnPoint();
        // Instatiate player into the game
        GameObject enemy = Instantiate(enemyPrefab, spm.spawnPoints[spawnPointIndex].position,
            enemyPrefab.transform.rotation);
        /* Reference to HealthBar component of the HealthBar gameObject.
        The HealthBar gameObject is actually a child of Stats which is a child
        of the enemy. */
        HealthBar hb = enemy.transform.GetChild(0).transform.GetChild(0).gameObject
            .GetComponent<HealthBar>();
        // Reference to the Player Health component of the enemy
        PlayerHealth ph = enemy.GetComponent<PlayerHealth>();
        // Reference to the Metadata component of the enemy
        Metadata md = enemy.GetComponent<Metadata>();
        // Set Player Health component Health Bar to healthbar gameobject
        if (ph != null && hb != null)
        {
            ph.healthBar = hb;
        }
        // Set Metadata component spawn point to spawnPointIndex
        if (md != null)
        {
            md.spawnPoint = spawnPointIndex;
        }
    }
}


