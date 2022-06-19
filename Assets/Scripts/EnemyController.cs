using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // References
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform playerT;
    [SerializeField] private LayerMask groundLayer, playerLayer;

    // Animation
    private Animator animController;
    private int velocityHash;
    private int attackHash;
    private int attackValHash;
    private float velocity = 0.0f;

    // Search
    [SerializeField] private Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    // Attack
    [SerializeField] private int damage = 10;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float hitDetectRange = 2.0f;
    private bool canAttack = true;

    // States
    [SerializeField] private float sightRange, attackRange;
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

    private void detectHit()
    {
        //print("Detect hit fired");
        // Detect if enemies (player) is hit
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,
            hitDetectRange, playerLayer);
        foreach (Collider enemy in hitEnemies)
        {
            //print(enemy);
            enemy.GetComponent<PlayerHealth>().applyDamage(damage);
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
            animController.SetInteger(attackValHash, attackIndex);
            // Trigger attack animation
            animController.SetTrigger(attackHash);
            // Delay hit dection until after the randomly selected animation
            // Swipe animation (len = 2.667, speed = 2.0)
            if (attackIndex == 0 || attackIndex == 1) StartCoroutine(delayDetect(2.667f, 2.0f));
            // Punch animation (len = 1.1, speed = 1.0)
            if (attackIndex == 2 || attackIndex == 3) StartCoroutine(delayDetect(1.1f, 1.0f));
        }
    }

    // Calls the detectHit function after an animations total time,
    // animations total time = (length of animation) * (1/anim-state-speed)
    IEnumerator delayDetect(float lenOfAnim, float animContSpeed)
    {
        float totalAnimTime = lenOfAnim * (1.0f/animContSpeed);
        yield return new WaitForSeconds(totalAnimTime);
        detectHit();
        canAttack = true;
    }
}
