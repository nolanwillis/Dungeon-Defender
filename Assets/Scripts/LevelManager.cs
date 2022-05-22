using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    // Reference to player prefab
    public GameObject playerPrefab;

    // GameObject references
    private GameObject mainCamera;
    private GameObject healthBar;

    // Component references
    private CameraFollow cf;
    private PlayerHealth ph;
    private HealthBar hb;
   
    // Location of spawn point
    public Transform spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Set Main Camera gameobject reference 
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        healthBar = GameObject.Find("HealthBar");
        
        // Set Camera Follow component reference
        cf = mainCamera.GetComponent<CameraFollow>();
        hb = healthBar.GetComponent<HealthBar>();
        // Spawn player
        spawn();
    }

    // Spawn, instantiates new player gameobject and sets camera follow/health bar values
    public void spawn()
    {
        // Instatiate player into the game
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, playerPrefab.transform.rotation);
        // Set Player Health component Health Bar to healthbar gameobject
        ph = player.GetComponent<PlayerHealth>();
        ph.healthBar = hb;
        // Set camera Camera Follow component Target to player transform
        cf.target = player.transform; 

    }

}
