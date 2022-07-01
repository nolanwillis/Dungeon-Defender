using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // References
    PlayerInputManager playerInputManager;
    PlayerLocomotion playerLocomotion;

    // Animation interacting state
    public bool isInteracting;
    private void Awake()
    {
        // Set component references
        // Player components
        playerInputManager = GetComponent<PlayerInputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        // Handle all input from player
        playerInputManager.HandleAllInput();
    }

    private void FixedUpdate()
    {
        // Handle everything related to player movement
        playerLocomotion.HandleAllMovement();
    }

}
