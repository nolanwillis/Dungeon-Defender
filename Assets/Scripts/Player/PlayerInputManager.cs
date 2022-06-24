using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // Component references
    [SerializeField] private PlayerInput playerInput;
    private PlayerAnimationManager playerAnimationManager;

    public Vector2 movementInput;
    public float horizontalInput;
    public bool jumpInput;
    

    private void Awake()
    {
        // Set component references
        // Player components
        playerAnimationManager = GetComponent<PlayerAnimationManager>();    
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
        horizontalInput = movementInput.x;
        playerAnimationManager.UpdateAnimatorParameters(Mathf.Abs(horizontalInput), true);
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
