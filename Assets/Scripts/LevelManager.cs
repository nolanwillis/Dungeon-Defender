using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    // Reference to player prefab
    public GameObject playerPrefab;
    // Reference to enemy prefab
    public GameObject enemyPrefab;
    // Location of spawn point
    public Transform[] spawnPoints;
    // Array that keeps track of which spawn points are in use
    static private bool[] openSpawns = new bool[10];
    // Integer thate keeps track of how many spawn points are left
    int numOpenSpawns = openSpawns.Length;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Set all spawn points to open
        for (int i = 0; i < openSpawns.Length; i++)
        {
            openSpawns[i] = true;
        }
        // Append all spawn points to an array of transforms
        spawnPoints = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        // Spawn prefabs
        spawnPlayer();
        for (int i = 0; i < 4; i++)
        {
            spawnEnemy();
        }
    }

    // spawnPlayer, instantiates New Player prefab
    public void spawnPlayer()
    {
        // Select random spawn point from available spawn points
        int randSpawnIndex = -1;
        if (numOpenSpawns > 0)
        {
            while (randSpawnIndex == -1)
            {
                int currRandomIndex = Random.Range(0, 10);
                if (openSpawns[currRandomIndex])
                {
                    randSpawnIndex = currRandomIndex;
                    openSpawns[currRandomIndex] = false;
                    numOpenSpawns--;
                }
            }
        }
        else
        {
            print("No available spawn point!");
            return;
        }
        // Instatiate player into the game
        GameObject player = Instantiate(playerPrefab, spawnPoints[randSpawnIndex].position, playerPrefab.transform.rotation);
        // Reference to the Camera Follow component of the Main Camera
        CameraFollow cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        // Reference to the Health Bar component of the Player Health Bar (the big one)
        HealthBar hb = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
        // Reference to the Player Health component of the player
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        // Reference to the Combat component of the player
        Combat cb = player.GetComponent<Combat>();
        // Set Player Health component Health Bar to healthbar gameobject
        if (ph != null && hb != null)
        {
            ph.healthBar = hb;
        }
        // Set Camera Camera Follow component Target to player transform
        if (cf != null && player != null)
        {
            cf.target = player.transform;
        }
        if (cb != null)
        {
            // Set Combat component Attack Point to the attackPoint gameobject transform
            cb.attackPoint = GameObject.Find("AttackPoint").transform; 
            // Set Combat component Enemy Layer to Enemies
            cb.enemyLayer = LayerMask.GetMask("Enemies");
        }
    }

    // spawnEnemy, instantiates Enemy prefab
    public void spawnEnemy()
    {
        // Select random spawn point from available spawn points
        int randSpawnIndex = -1;
        if (numOpenSpawns > 0)
        {
            while (randSpawnIndex == -1)
            {
                int currRandomIndex = Random.Range(0, 10);
                if (openSpawns[currRandomIndex])
                {
                    randSpawnIndex = currRandomIndex;
                    openSpawns[currRandomIndex] = false;
                    numOpenSpawns--;
                }
            }
        }
        else
        {
            print("No available spawn point!");
            return;
        }
        // Instatiate player into the game
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[randSpawnIndex].position, enemyPrefab.transform.rotation);
        /* Reference to HealthBar component of the HealthBar gameObject.
        The HealthBar gameObject is actually a child of Stats which is a child
        of the enemy. */
        HealthBar hb = enemy.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<HealthBar>();
        // Reference to the Player Health component of the enemy
        PlayerHealth ph = enemy.GetComponent<PlayerHealth>();
        // Set Player Health component Health Bar to healthbar gameobject
        if (ph != null && hb != null)
        {
            ph.healthBar = hb;
        }
    }
}


