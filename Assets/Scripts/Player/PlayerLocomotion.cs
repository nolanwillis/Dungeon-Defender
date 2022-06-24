using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    // Component references
    private PlayerManager playerManager;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerInputManager playerInputManager;
    private Rigidbody playerRigidbody;

    [Header("Movement Flags")]
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Values")]
    [SerializeField] private float playerMovementSpeed = 7.0f;
    [SerializeField] private float playerAcceleration = 2.0f;
    [SerializeField] private float playerDecceleration = 5.0f;
    [SerializeField] private float playerRotationSpeed = .15f;

    [Header("Falling")]
    [SerializeField] private float inAirTimer;
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallingVelocity;
    [SerializeField] private float rayCastHeightOffset = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float airControl;

    private void Awake()
    {
        // Set component references
        // Player components
        playerManager = GetComponent<PlayerManager>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        // Handle falling and landing
        HandleFallingAndLanding();
        // If player is interacting disable all movement
        if (playerManager.isInteracting) return;
        // Handle lateral movement
        HandleMovement();
        // Handle rotation
        HandleRotation();
    }

    private void HandleMovement()
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
    }

    private void HandleRotation()
    {
        Quaternion targetRotation;
        if (playerInputManager.horizontalInput != 0)
        {
            // Target rotation if there's horizontal input
            targetRotation =
            Quaternion.Euler(0.0f, Mathf.Sign(playerInputManager.horizontalInput) * 90.0f, 0.0f);
        }
        else
        {
            // Target rotation if there's no horizontal input
            targetRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        // Rotate player;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
            playerRotationSpeed);
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += rayCastHeightOffset;
        
        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                playerAnimationManager.PlayTargetAnimation("falling", true);
            }
            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.5f, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                playerAnimationManager.PlayTargetAnimation("landing", true);
            }
            inAirTimer = 0.0f;
            isGrounded = true;
            playerManager.isInteracting = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            playerAnimationManager.playerAnimatorController.SetBool("isJumping", true);
            playerAnimationManager.PlayTargetAnimation("jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravitIntensity * jumpHeight);
        }
    }
}
