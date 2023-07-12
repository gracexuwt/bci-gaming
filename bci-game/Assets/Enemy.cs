using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float attackRange = 1f; 
    public int attackDamage = 10; 
    public float attackCooldown = 2f; 
    public Transform frontCheck; 
    public LayerMask playerLayer; 

    private bool canAttack = true; 
    private bool facingRight = true; 

    private void Awake()
    {
        playerLayer = LayerMask.GetMask("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        float[] inputs = GetInput();

        float hChange = UpdateHorizontal(inputs[0]) * Time.deltaTime;
        
        // Flip movement direction if enemy encounters an obstacle
        if (hChange > 0 && movementBlocked[2] > 0)
        {
            hChange = 0;
            Flip();
        }
        else if (hChange < 0 && movementBlocked[3] > 0)
        {
            hChange = 0;
            Flip();
        }

        float vChange = UpdateVertical() * Time.deltaTime;
        if (vChange > 0 && movementBlocked[0] > 0) vChange = 0;
        else if (vChange < 0 && movementBlocked[1] > 0) vChange = 0;

        StartCoroutine(MoveCharacter(new Vector3(hChange, vChange, 0)));

        if (canAttack && IsPlayerInAttackRange())
        {
            PerformAttack();
        }
    }

    IEnumerator AttackCooldown()
    {
        while (true)
        {
            canAttack = false;
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
            yield return null;
        }
    }

    void PerformAttack()
    {
        if (IsPlayerInAttackRange())
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    Player player = hitCollider.GetComponent<Player>();
                    Debug.Log("Player hit");
                    break;
                }
            }
        }
    }

    public override float[] GetInput()
    {
        if (facingRight)
            return new float[] { 1, 0 }; 
        else
            return new float[] { -1, 0 }; 
    }
    public bool IsPlayerInAttackRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(frontCheck.position, attackRange, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}