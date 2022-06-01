using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private float attackRange = .5f;
    [SerializeField] private LayerMask enemyLayer;
    public Transform attackPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackRange = .5f;
            Attack();
        }
        if (Input.GetKeyDown("w"))
        {
            attackRange = 2.0f;
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
