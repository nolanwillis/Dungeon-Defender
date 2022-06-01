using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed
    [SerializeField] private float moveSpeed = 8.0f;
    // Acceleration
    [SerializeField] private float acc = 2.0f;
    // Decceleration (Greater the number, faster the stop)
    [SerializeField] private float dcc = 5.0f;
    // Jump force
    [SerializeField] private float jumpForce = 6.0f;
    // Air control 
    [SerializeField] private float airControl = 2.0f;
    // Coyote time 
    [SerializeField] private float coyoteTime = 0.2f;
    // Jump buffer period
    [SerializeField] private float jumpBufferTime = 0.2f;
    // Gravity value
    [SerializeField] private float fallForceMultiplier = 20.0f;
    // Turn smooth speed
    [SerializeField] private float turnSmoothSpeed = 2.1f;
    // Default player rotation
    private Quaternion defaultPRot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    // Coyote time counter
    private float coyoteTimeCounter = 0;
    // Last time on ground
    private float? lastGroundedTime;
    // Last time jump button was pressed
    private float? jumpLastPressedTime;
    // Keeps track of if the player is on the ground
    private bool isGrounded;
    // Reference to Rigidbody component of player;
    private Rigidbody rb;
    // Reference to Collider component of player;
    private CapsuleCollider cc;
    
    // Start, called before everything
    void Start()
    {
        // Set component references
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        transform.rotation = defaultPRot;
    }

    // Collision Functions
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && rb.velocity.y == 0)
        {
            isGrounded = true;
        }
    }
   
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    // FixedUpdate, runs the same speed as physics engine
    void FixedUpdate()
    {
        // Ensure RigidBody component exists
        if (rb != null)
        {
            // Lateral Movement
            float moveInput = Input.GetAxis("Horizontal");
            // Calculate target speed and direction (+ right, - left);
            float targetSpeed = moveInput * moveSpeed;
            // Calculate difference in current speed and target speed
            float speedDif = targetSpeed - rb.velocity.x;
            // Select correct acceleration rate (if btn is pressed acc)
            float accRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acc : dcc;
            // Calculate the magnitude of the applied force
            float movement = Mathf.Abs(speedDif) * accRate * Mathf.Sign(speedDif);
            // Apply lateral force to the players Rigidbody component
            rb.AddForce(movement * Vector3.right);


            if (Input.GetKey("d") || Input.GetKey("a"))
            {
                Quaternion movDir = Quaternion.Euler(0.0f, Mathf.Sign(moveInput) * 90.0f, 0.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, movDir, turnSmoothSpeed);
            }
           
            
            
            // In Air Control Dampening
            if (!isGrounded &&  (rb.velocity.x <= -0.01f || rb.velocity.x >= 0.01f))
            {
                rb.AddForce(-Mathf.Sign(speedDif) * airControl * Vector3.right);
            }

            // Increase fall gravity
            if (rb.velocity.y < 0)
            {
                rb.AddForce(fallForceMultiplier * Vector3.down);
            }
        }
    }

    // Update, called every frame
    void Update()
    {
        // Jump, with coyote time and jump buffering
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            lastGroundedTime = Time.time;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpLastPressedTime = Time.time;
        }
        if (Time.time - jumpLastPressedTime <= jumpBufferTime && 
            Time.time - lastGroundedTime <= jumpBufferTime && coyoteTimeCounter > 0.0f && rb.velocity.y <= 0)
        {
            Jump();
            coyoteTimeCounter = 0.0f;
            jumpLastPressedTime = null;
            lastGroundedTime = null;
        }

    }

    // Jump, applies jump force to player
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
