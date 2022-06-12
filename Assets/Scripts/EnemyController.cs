using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // References
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform playerT;
    public LayerMask groundLayer, playerLayer;

    // Animation
    Animator animController;
    private int velocityHash;
    private int attackHash;
    private int attackValHash;
    private float velocity = 0.0f;
    private int prevAttack = -1;

    // Search
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attack
    public float timeBetweenAttacks;
    bool hasAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        animController = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
        attackHash = Animator.StringToHash("Attack");
        attackValHash = Animator.StringToHash("attackVal");
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
        animController.SetFloat(velocityHash, velocity);
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

    private int getRandomAttack()
    {
        // Return value
        int result = -1;
        // While result == prevAttack, find a new attack 
        while (result == prevAttack)
        {
            result = Random.Range(0, 5);
        }
        prevAttack = result;
        return result;
    }
    
    private void Attack()
    {
        // If attacking don't move
        agent.SetDestination(transform.position);
        // If attacking look at player
        transform.LookAt(playerT);
        // If not currently attacking, or in the time between attacks window, attack
        if (!hasAttacked)
        {
            animController.SetInteger(attackValHash, getRandomAttack());
            // Trigger attack animation
            animController.SetTrigger(attackHash);
            hasAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }
}
