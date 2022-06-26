using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Calls all handler functions that are triggered by input
    // Component references
    PlayerInputManager playerInputManager;
    PlayerLocomotion playerLocomotion;
    Animator playerAnimatorController;

    // Animation interacting state
    public bool isInteracting;
    private void Awake()
    {
        // Set component references
        // Player components
        playerInputManager = GetComponent<PlayerInputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAnimatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        // Handle all input from player
        playerInputManager.HandleAllInput();
    }

    private void FixedUpdate()
    {
        // Handle all player movement
        playerLocomotion.HandleAllMovement();
    }

}
