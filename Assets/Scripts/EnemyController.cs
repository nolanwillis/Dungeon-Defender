using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerT;
    public LayerMask groundLayer, playerLayer;

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
        playerT = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        // Check if player is in sight/attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange,
            playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange,
            playerLayer);

        // Depending on sight/attack range set current state
        if (!playerInSightRange && !playerInAttackRange) Search();
        if (playerInSightRange && !playerInAttackRange) Follow();
        if (playerInSightRange && playerInAttackRange) Attack();

    }

    private void Search()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        // Distance to walkPoint
        Vector3 distanceToWP = transform.position - walkPoint;

        // walkPointReached
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
    
    private void Attack()
    {
        // If attacking don't move
        agent.SetDestination(transform.position);
        // If attacking look at player
        transform.LookAt(playerT);

        if (!hasAttacked)
        {
            // todo: set attack animations/logic

            hasAttacked = true;
           
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }
}
