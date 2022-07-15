using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // References
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform playerT;
    private AudioManager audioManager;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask playerLayer;

    // Animation
    private Animator animatorController;
    private int velocityHash = Animator.StringToHash("Velocity");
    private int attackHash = Animator.StringToHash("Attack");
    private int attackValHash = Animator.StringToHash("attackVal");
    private int playerIsBlockingHash = Animator.StringToHash("isBlocking");
    private float velocity = 0.0f;

    [Header("Searching")]
    [SerializeField] private Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    [Header("Attacking")]
    [SerializeField] private int damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float hitDetectRange;
    public bool canAttack = true;

    [Header("Range")]
    [SerializeField] private float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        // Set references
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animatorController = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate()
    {
        // Set player transform, must be in update in case respawn
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        // Check if player is in sight/attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange,
            playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange,
            playerLayer);
        // Set velocity in the enemyAnimationController
        velocity = Mathf.Abs(agent.velocity.magnitude);
        animatorController.SetFloat(velocityHash, velocity);
        // Depending on sight/attack range set current state
        if (!playerInSightRange && !playerInAttackRange) Search();
        if (playerInSightRange && !playerInAttackRange) Follow();
        if (playerInSightRange && playerInAttackRange) Attack();
    }

    private void Search()
    {
        // If walkpoint not set find a walkpoint
        if (!walkPointSet) SearchWalkPoint();
        // If walkpoint set go to walkpoint
        if (walkPointSet) agent.SetDestination(walkPoint);
        // Set current distance to walkPoint
        Vector3 distanceToWP = transform.position - walkPoint;
        // Determine if walkPointReached
        if (distanceToWP.magnitude < 1.0f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        // Set walkPoint
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
            transform.position.z);
        // Check if point is near/over ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void Follow()
    {
        agent.SetDestination(playerT.position);
    }

    public void DetectHit()
    {
        // Detect if enemies (player) is hit
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,
            hitDetectRange, playerLayer);
        foreach (Collider enemy in hitEnemies)
        {
            Animator playerAnimatorController = enemy.GetComponent<Animator>();
            if (playerAnimatorController.GetBool(playerIsBlockingHash))
            {
                playerAnimatorController.CrossFade("blockedImpact", 0.1f);
                audioManager.Play("shieldHit");
            }
            else
            {
                enemy.GetComponent<PlayerHealth>().ApplyDamage(damage);
                audioManager.Play("punchImpact");
                audioManager.Play("playerPain");
            }
        }
    }

    private void Attack()
    {
        // If attacking don't move
        agent.SetDestination(transform.position);
        // If attacking make sure enemy is looking at player
        transform.LookAt(playerT);
        if (canAttack)
        {
            canAttack = false;
            // Get a random attack animation index
            int attackIndex = Random.Range(0, 4);
            // Set random attack animation
            animatorController.SetInteger(attackValHash, attackIndex);
            // Trigger attack animation
            animatorController.SetTrigger(attackHash);
            // Reset can attack 
            StartCoroutine(ResetCanAttack(2.25f));
        }
    }

    // Coroutine that resets the can attack flag after a given delay time.
    IEnumerator ResetCanAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        canAttack = true;
    }

    // Function that calls delay detect, required so delay detect hit can be called
    // from the detect hit state machine behaviour script.
    public void StartDelayDetectHit(float delayTime)
    {
        StartCoroutine(DelayDetectHit(delayTime));
    }
    
    // Corouting that waits to fire detect hit just before the end of an attack animation.
    IEnumerator DelayDetectHit(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        DetectHit();
    }
}
