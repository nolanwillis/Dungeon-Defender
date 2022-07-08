using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // References
    PlayerInputManager playerInputManager;
    PlayerLocomotion playerLocomotion;

    [Header("Flags")]
    public bool isDead = false;

    private void Awake()
    {
        // Set component references
        // Player components
        playerInputManager = GetComponent<PlayerInputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        if (!isDead)
        {
            // Handle all input from player
            playerInputManager.HandleAllInput();
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            // Handle everything related to player movement
            playerLocomotion.HandleAllMovement();
        }
    }
}
