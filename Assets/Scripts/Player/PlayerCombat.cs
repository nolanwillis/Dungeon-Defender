using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // References
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;
    private Animator playerAnimatorController;

    [Header("Combat Flags")]
    [SerializeField] private bool isBlocking;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isCasting;
    [SerializeField] private bool Hit1;
    [SerializeField] private bool Hit2;

    // Animator hashes
    private int isBlockingHash = Animator.StringToHash("isBlocking");
    private int isAttackingHash = Animator.StringToHash("isAttacking");
    private int isCastingHash = Animator.StringToHash("isCasting");

    // Radius of hit detection sphere cast
    private float attackRange;

    private void Awake()
    {
        playerAnimatorController = GetComponent<Animator>();
        
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

            attackRange = 0.5f;
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
 
    public void HandleCast()
    {
        attackRange = 1.5f;
        playerAnimatorController.SetBool(isCastingHash, true);
        DetectHit(50);
    }

    private void DetectHit(int damage)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,
            attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            // Debug.Log("Hit: " + enemy.name);
            enemy.GetComponent<PlayerHealth>().applyDamage(damage);
        }
    }
}
