using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed
    [SerializeField] private float moveSpeed = 15.0f;
    // Acceleration
    [SerializeField] private float acc = 5.0f;
    // Decceleration
    [SerializeField] private float dcc = 0.25f;
    // Jump force
    [SerializeField] private float jumpForce = 6.0f;
    //private float lastGroundedTime = 0;
    //private float lastJumpTime = 0;
    //private bool isJumping = false;
    //private bool jumpInputReleased = true;
    // Reference to Rigidbody component of player;
    private Rigidbody rb;
    // Reference to Collider component of player;
    private CapsuleCollider cc;
    
    
        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    // FixedUpdate, called 50 times per second
    void FixedUpdate()
    {
        // Ensure RigidBody component exists
        if (rb != null)
        {
            float moveInput = Input.GetAxis("Horizontal");
            // Calculate target speed and direction (+ right, - left);
            float targetSpeed = moveInput * moveSpeed;
            // Calculate difference in current speed and target speed
            float speedDif = targetSpeed - rb.velocity.x;
            // Select correct acceleration rate (if btn is pressed acc)
            float accRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acc : dcc;
            // Calculate the magnitude of the applied force
            float movement = Mathf.Abs(speedDif) * accRate * Mathf.Sign(speedDif);

            // Apply the force to the players Rigidbody component
            rb.AddForce(movement * Vector3.right);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //lastGroundedTime = 0;
        //lastJumpTime = 0;
        //isJumping = true;
        //jumpInputReleased = false;
    }
}
