using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{   
    private GameObject camera;

    public float attackRange = 1f; 
    public int attackDamage = 10; 
    public float attackCooldown = 1f; 
    public Transform frontCheck; 
    private bool canAttack = true; 

    public override float[] GetInput() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        if (hInput != 0) {
            animator.SetFloat("X", hInput);
        }

        if (Input.GetMouseButton(0)){
            if (canAttack){
                PerformAttack();
            }
        }

        return new float[] {hInput, vInput};
    }

    // protected override IEnumerator MoveCharacter(Vector3 movement){
    //     if (camera == null) camera = GameObject.FindWithTag("MainCamera");
    //     transform.position += movement;
    //     camera.transform.position += movement;
    //     yield return null;
    // }

    protected override IEnumerator MoveCharacter(Vector3 movement)
{
    if (camera == null) 
        camera = GameObject.FindWithTag("MainCamera");

    // Move the character in both X and Y axes
    transform.position += movement;

    // Fix the camera's position in the Y-axis while allowing movement in the X-axis
    camera.transform.position += new Vector3(movement.x, 0f, 0f);

    yield return null;
}

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        yield return null;
    }

    void PerformAttack()
    {
        if (canAttack)
        {
            StartCoroutine(AttackCooldown()); // prevent further attacks during cooldown

            //flip hitbox if it is facing the wrong direction
            float hitboxLoc = frontCheck.position.x - transform.position.x;
            if ((animator.GetFloat("X") > 0.5) ^ (hitboxLoc > 0)) {//XOR
                frontCheck.position -= new Vector3(hitboxLoc * 2, 0, 0);
            }


            animator.SetTrigger("isMelee");
            Collider[] hitColliders = Physics.OverlapSphere(frontCheck.position, attackRange);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    Enemy enemy = hitCollider.GetComponent<Enemy>();
                    Debug.Log("ENEMY HIT");
                    break; // Exit the loop as soon as a player is found
                }
            }
        }
    }

}
