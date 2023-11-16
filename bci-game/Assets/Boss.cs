using System.Collections;
using UnityEngine;

public class Boss : Character
{
    public int maxHealth = 100;
    public int currentHealth;
    public float attackRange = 3.0f;
    public float attackCooldown = 2.0f;
    public Transform target; // The player or target the boss will attack

    private bool isAttacking = false;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        // Initialize other components and variables as needed.
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (!isAttacking)
        {
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                // The boss is in range to attack the player
                StartCoroutine(Attack());
            }
            else
            {
                // Move towards the player
                MoveTowardsTarget();
            }
        }
    }

    private void MoveTowardsTarget()
    {
        // Implement code to move the boss towards the target here.
        // You can use Vector3.MoveTowards or a similar method.
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); // Trigger the boss's attack animation

        // Implement code to deal damage to the target here.
        // You may use a Coroutine for this and add damage logic.

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    private void Die()
    {
        // Implement code for boss's death, like playing a death animation, spawning loot, etc.
        // You might want to destroy or deactivate the boss object after a death animation.
    }
}

