using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator playerAnimatorController;
    private int velocityXHash, isInteractingHash, isJumpingHash, 
        crouchHash, attackHash, blockHash, castHash;

    private void Awake()
    {
        // Set component references
        // Player components
        playerAnimatorController = GetComponent<Animator>();
        // Set hash values for the player animator controller parameters
        velocityXHash = Animator.StringToHash("VelocityX");
        isInteractingHash = Animator.StringToHash("isInteracting");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        playerAnimatorController.SetBool(isInteractingHash, isInteracting);
        playerAnimatorController.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorParameters(float velocityX, bool isSprinting)
    {
        playerAnimatorController.SetFloat(velocityXHash, velocityX, 0.1f, Time.deltaTime);
    }
}
