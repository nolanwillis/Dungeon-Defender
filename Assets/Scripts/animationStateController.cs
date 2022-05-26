using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class animationStateController : MonoBehaviour
{
    Animator animController;
    private int velocityHash;
    private int jumpHash;
    private int crouchHash;
    private int attackHash;
    private float velocity = 0.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
        jumpHash = Animator.StringToHash("Jump");
        crouchHash = Animator.StringToHash("Crouch");
        attackHash = Animator.StringToHash("Attack");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set velocity of animation controller to player velocity from 
        // the Player Movement component
        velocity = Math.Abs(rb.velocity.x);
        animController.SetFloat(velocityHash, velocity);

        // Set jump to true if player hits space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animController.SetTrigger(jumpHash);
        }

        // Set crouch to true if player hits c
        if (Input.GetKey("c"))
        {
            animController.SetBool(crouchHash, true);
        }
        else
        {
            animController.SetBool(crouchHash, false);
        }

        // Set attack to true if player hits left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animController.SetTrigger(attackHash);
        }
        
    }
}

// run, jump, crouch2, slash
