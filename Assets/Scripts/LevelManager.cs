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
    }

    // spawnPlayer, instantiates New Player gameObject
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
        // Component references needed when spawning the player
        CameraFollow cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        HealthBar hb = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
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

    // spawnEnemy, instantiates Enemy gameObject
    public void spawnEnemy()
    {
        
    }

}
