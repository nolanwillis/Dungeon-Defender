using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private float attackRange;

    // References
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;
    private Animator playerAnimatorController;

    [Header("Combat Flags")]
    [SerializeField] private bool isBlocking;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isCasting;
    [SerializeField] private bool canAttack = true;

    // Animator hashes
    private int isBlockingHash = Animator.StringToHash("isBlocking");
    private int isAttackingHash = Animator.StringToHash("isAttacking");
    private int isCastingHash = Animator.StringToHash("isCasting");
    private int hitCounterHash = Animator.StringToHash("hitCounter");

    // Hit counter
    private int hitCounter = 0;

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
        if (canAttack)
        {
            canAttack = false;
            attackRange = 0.5f;
            playerAnimatorController.SetBool(isAttackingHash, true);
            StartCoroutine(DelayDetect(1.5f, 1.25f));
        }
    }
    
    public void HandleCast()
    {
        attackRange = 1.5f;
        playerAnimatorController.SetBool(isCastingHash, true);
        StartCoroutine(DelayDetect(1.5f, 1.25f));
    }

    private void DetectHit(int damage)
    {
        print("detect hit called");
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,
            attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
            enemy.GetComponent<PlayerHealth>().applyDamage(damage);
            if (hitCounter < 3)
            {
                hitCounter++;
                playerAnimatorController.SetInteger(hitCounterHash, hitCounter);
            }
            else
            {
                hitCounter = 0;
                playerAnimatorController.SetInteger(hitCounterHash, hitCounter);
            }
        }
    }

    IEnumerator DelayDetect(float lenOfAnim, float animContSpeed)
    {
        float totalAnimTime = lenOfAnim * (1.0f / animContSpeed);
        yield return new WaitForSeconds(totalAnimTime);
        DetectHit(25);
        canAttack = true;
    }
}
