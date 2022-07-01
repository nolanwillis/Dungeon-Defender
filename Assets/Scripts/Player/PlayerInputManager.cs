using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // References
    [SerializeField] private PlayerInput playerInput;
    private PlayerLocomotion playerLocomotion;
    private PlayerCombat playerCombat;

    [Header("Locomotion")]
    public Vector2 movementInput;
    public float horizontalInput;
    public bool jumpInput;

    [Header("Combat")]
    public bool blockPressInput;
    public bool blockReleaseInput;
    public bool attackInput;
    public bool canAttack = true;
    public bool castInput;
    
    private void Awake()
    {
        // Set references
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            // If player input is not instantiated create a new player input object
            playerInput = new PlayerInput();
            // Read input values
            playerInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInput.PlayerActions.Jump.performed += i => jumpInput = true;
            playerInput.PlayerActions.BlockPress.performed += i => blockPressInput = true;
            playerInput.PlayerActions.BlockRelease.performed += i => blockReleaseInput = true;
            playerInput.PlayerActions.Attack.performed += i => attackInput = true;
            playerInput.PlayerActions.Cast.performed += i => castInput = true;
        }
        // Enable the player input object
        playerInput.Enable();
    }
    
    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void HandleAllInput()
    {
        // Movement
        HandleMovementInput();
        // Actions
        HandleActionInput();
    }

    private void HandleMovementInput()
    {
        // Set horizontal input value
        horizontalInput = movementInput.x;
    }

    private void HandleActionInput()
    {
        if (jumpInput)
        {
            // Reset jump input value
            jumpInput = false;
            // Call handle jump in locomotion
            playerLocomotion.HandleJump();
        }
        if (blockPressInput)
        {
            // Reset block input value
            blockPressInput = false;
            // Call handle block press in combat
            playerCombat.HandleBlockPress();
        }
        if (blockReleaseInput)
        {
            // Reset block release input value
            blockReleaseInput = false;
            // Call handle block release in combat
            playerCombat.HandleBlockRelease();
        }
        if (attackInput && canAttack && playerLocomotion.isGrounded)
        {
            // Set can attack to false
            canAttack = false;
            // Reset attack input value
            attackInput = false;
            // Call handle attack in combat
            if (horizontalInput == 0)
            {
                playerCombat.HandleAttack();
            } else
            {
                playerCombat.HandleRunningAttack();
            }
            // Reset can attack
            StartCoroutine(ResetCanAttack(.75f));
        }
        if (castInput)
        {
            // Reset cast input value
            castInput = false;
            // Call handle cast in combat
            playerCombat.HandleCast();
        }
    }

    // Coroutine that resets the can attack flag after a given delay time
    IEnumerator ResetCanAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        canAttack = true;
    }
}
