using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // References
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;
    private AudioManager audioManager;
    private Animator playerAnimatorController;

    [Header("Combat Flags")]
    [SerializeField] private bool isBlocking;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool Hit1;
    [SerializeField] private bool Hit2;

    // Animator hashes
    private int isBlockingHash = Animator.StringToHash("isBlocking");
    private int isAttackingHash = Animator.StringToHash("isAttacking");

    // Radius of hit detection sphere cast
    private float attackRange;

    private void Awake()
    {
        // Set references
        playerAnimatorController = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void HandleBlockPress()
    {
        playerAnimatorController.SetBool(isBlockingHash, true);
    }

    public void HandleBlockRelease()
    {
        playerAnimatorController.SetBool(isBlockingHash, false);
    }

    public void HandleAttack()
    {
        if (!playerAnimatorController.GetBool(isAttackingHash))
        {
            // Set global attack range variable
            attackRange = 0.5f;
            // Set is attacking parameter in the player animator controller to true
            playerAnimatorController.SetBool(isAttackingHash, true);
            
            if (!Hit1 && !Hit2)
            {
                Hit1 = true;
                playerAnimatorController.CrossFade("attack", 0.1f);
            }
            else if (Hit1 && !Hit2)
            {
                Hit2 = true;
                playerAnimatorController.CrossFade("attack2", 0.1f);
            }
            else
            {
                Hit1 = false;
                Hit2 = false;
                playerAnimatorController.CrossFade("shieldAttack", 0.1f);
            }
            DetectHit(25);
        }
    }

    public void HandleRunningAttack()
    {
        if (!playerAnimatorController.GetBool(isAttackingHash))
        {

            attackRange = 0.5f;
            playerAnimatorController.SetBool(isAttackingHash, true);
            if (!Hit1 && !Hit2)
            {
                Hit1 = true;
                playerAnimatorController.CrossFade("runningAttack", 0.1f);
            }
            else if (Hit1 && !Hit2)
            {
                Hit2 = true;
                playerAnimatorController.CrossFade("runningAttack2", 0.1f);
            }
            else
            {
                Hit1 = false;
                Hit2 = false;
                playerAnimatorController.CrossFade("runningShieldAttack", 0.1f);
            }
            DetectHit(15);
        }
    }

    private void DetectHit(int damage)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,
            attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            // Apply damage to enemy
            enemy.GetComponent<PlayerHealth>().applyDamage(damage);
            // Play sword hit sound
            audioManager.Play("fleshHit");
        }
    }
}
