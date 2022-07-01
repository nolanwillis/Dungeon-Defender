using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    // All handler functions related to locomotion(jumping, falling, landing, rotating, moving)
    // References
    private PlayerManager playerManager;
    private Animator playerAnimatorController;
    private PlayerInputManager playerInputManager;
    private Rigidbody playerRigidbody;

    [Header("Movement Flags")]
    public bool isGrounded;

    [Header("Movement Values")]
    [SerializeField] private float playerMovementSpeed = 7.0f;
    [SerializeField] private float playerAcceleration = 2.0f;
    [SerializeField] private float playerDecceleration = 5.0f;
    [SerializeField] private float playerRotationSpeed = .15f;

    [Header("Falling")]
    [SerializeField] private float inAirTimer;
    [SerializeField] private float inAirControl;
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallingVelocity;
    [SerializeField] private float rayCastHeightOffset = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject ledgeRayUpper;
    [SerializeField] private GameObject ledgeRayLower;
    
    // Animation Hashes
    private int velocityXHash = Animator.StringToHash("velocityX");
    private int isGroundedHash = Animator.StringToHash("isGrounded");

    private float previousDirection = 1;

    private void Awake()
    {
        // Set component references
        // Player components
        playerManager = GetComponent<PlayerManager>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerAnimatorController = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Calls all movement functions (non-actions only)
    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        HandleLateralMovement();
        HandleRotation();
    }

    // Movement functions
    private void HandleLateralMovement()
    {
        // Calculate player's target velocity 
        float playerTargetVelocity = playerInputManager.horizontalInput * playerMovementSpeed;
        // Calculate difference between player's current velocity and target velocity
        float playerVelocityDelta = playerTargetVelocity - playerRigidbody.velocity.x;
        // Determine player's current acceleration state
        float playerAccelerationState = Mathf.Abs(playerTargetVelocity) > 0.01f ?
            playerAcceleration : playerDecceleration;
        // Calculate the magnitude of the lateral force applied to the player
        float playerLateralForce = playerVelocityDelta * playerAccelerationState;
        // Apply the lateral force to the player
        playerRigidbody.AddForce(playerLateralForce * Vector3.right);
        // Update velocity parameter in the player animator contoller
        playerAnimatorController.SetFloat(velocityXHash, Mathf.Abs(playerInputManager.horizontalInput));
    }

    private void HandleRotation()
    {
        // Target rotation of player
        Quaternion targetRotation;
        if (playerInputManager.horizontalInput != 0)
        {
            // If there's input set target rotation to 90 * horizontal input
            targetRotation = Quaternion.Euler(0.0f, 
                Mathf.Sign(playerInputManager.horizontalInput) * 90.0f, 0.0f);
            // Update the previous direction variable
            previousDirection = playerInputManager.horizontalInput;
        }
        else
        {
            // If there's no horizontal input set rotation to previous direction
            targetRotation = Quaternion.Euler(0.0f, previousDirection * 90.0f, 0.0f);
        }
        // Rotate player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
            playerRotationSpeed);
    }

    private void HandleFallingAndLanding()
    {
        // Raycast variables for ground detection 
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += rayCastHeightOffset;

        // Ground detection logic
        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.7f, groundLayer))
        {
            // Reset in air timer
            inAirTimer = 0.0f;
            // Update grounded flag
            isGrounded = true;
            // Update velocity parameter in the player animator controller
            playerAnimatorController.SetBool(isGroundedHash, true);
        }
        else
        {
            // Update the grounded flag
            isGrounded = false;
            // Update velocity parameter in the player animator controller
            playerAnimatorController.SetBool(isGroundedHash, false);
            // Update in air timer
            inAirTimer = inAirTimer + Time.deltaTime;
            // Add leaping force when leaving a surface
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            // Increase gravitonal force based on time in air
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
            // If theres horizontal input apply dampening force in opposite direction of movement
            if (playerInputManager.horizontalInput != 0)
            {
                playerRigidbody.AddForce(-Mathf.Sign(playerInputManager.horizontalInput) * inAirControl 
                    * Vector3.right);
            }
        }
    }

    // Action functions
    public void HandleJump()
    {
        // Only jump if player is on ground
        if (isGrounded)
        {
            // Add jump force to player
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Trigger jump animation
            playerAnimatorController.CrossFade("jump", 0.1f);
        }  
        // Ledge assist
        if (Physics.Raycast(ledgeRayLower.transform.position, Vector3.forward, 0.5f, groundLayer))
        {
            print("Lower ledge ray hit");
            if (!Physics.Raycast(ledgeRayUpper.transform.position, Vector3.forward, 0.5f, groundLayer))
            {
                print("Upper ledge ray hit");
                playerRigidbody.AddForce(Vector3.up * 0.6f, ForceMode.Impulse);
                playerRigidbody.AddForce(Vector3.forward * 1.1f * playerInputManager.horizontalInput, 
                    ForceMode.Impulse);
            }
        }
    }
}
