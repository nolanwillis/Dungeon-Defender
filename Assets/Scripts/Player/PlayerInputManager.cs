using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // Enables unity's input system, reads and handles user inputs
    // Component references
    [SerializeField] private PlayerInput playerInput;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerLocomotion playerLocomotion;

    public Vector2 movementInput;
    public float horizontalInput;
    public bool jumpInput;
    

    private void Awake()
    {
        // Set component references
        // Player components
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInput.PlayerActions.Jump.performed += i => jumpInput = true;
        }
        playerInput.Enable();
    }
    
    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleJumpInput();
        // Handle action....etc
    }

    private void HandleMovementInput()
    {
        // Set horizontal input variable which is read in locomotion
        horizontalInput = movementInput.x;
        
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJump();
        }
    }
}
