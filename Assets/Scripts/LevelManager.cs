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
    private GameObject attackPoint;

    // Component references
    private CameraFollow cf;
    private PlayerHealth ph;
    private HealthBar hb;
    private Combat cb;
   
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
        
        // Set reference for main camera and health bar
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
        // Set reference to required gameObjects attached to player
        attackPoint = GameObject.Find("AttackPoint");
        // Set references to required components of the player
        ph = player.GetComponent<PlayerHealth>();
        cb = player.GetComponent<Combat>();
        // Set Player Health component Health Bar to healthbar gameobject
        if (hb != null)
        {
            ph.healthBar = hb;
        }
        // Set Camera Camera Follow component Target to player transform
        if (player != null)
        {
            cf.target = player.transform;
        }
        // Set Combat component Attack Point to the attackPoint gameobject transform
        if (attackPoint != null)
        {
            cb.attackPoint = attackPoint.transform;
        }

    }

}
