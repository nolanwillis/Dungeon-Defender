using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private float attackRange = .5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackRange = .5f;
            Attack(50);
        }
        if (Input.GetKeyDown("w"))
        {
            attackRange = 2.0f;
            Attack(100);
        }
    }

    private void Attack(int damage)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,
            attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
            enemy.GetComponent<PlayerHealth>().applyDamage(damage);
        }
    }
}
