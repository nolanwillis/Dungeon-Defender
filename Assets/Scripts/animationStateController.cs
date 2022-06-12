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
    private int blockHash;
    private int castHash;
    private float velocity = 0.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        animController = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
        jumpHash = Animator.StringToHash("Jump");
        crouchHash = Animator.StringToHash("Crouch");
        attackHash = Animator.StringToHash("Attack");
        blockHash = Animator.StringToHash("Block");
        castHash = Animator.StringToHash("Cast");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Set velocity of animation controller to player velocity from 
        // the Rigidbody component
        velocity = Math.Abs(rb.velocity.x);
        animController.SetFloat(velocityHash, velocity);

        // Input
        // Trigger jump if player hits space
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

        // Trigger attack if player hits left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animController.SetTrigger(attackHash);
        }

        // Set block to true if player hits right click
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animController.SetBool(blockHash, true);
        }
        else
        {
            animController.SetBool(blockHash, false);
        }

        // Trigger cast if player hits w
        if (Input.GetKeyDown("w"))
        {
            animController.SetTrigger(castHash);
        }

    }
}
