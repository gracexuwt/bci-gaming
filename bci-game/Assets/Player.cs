using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private GameObject camera;
    private bool isFlipping = false; // Track if currently flipping
    private float flipDelay = 0.2f; // Delay in seconds before flip occurs
    private bool facingRight = true; // Track character's facing direction


    public override float[] GetInput()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        // Check if direction change is needed
        if (hInput != 0 && ((hInput > 0 && !facingRight) || (hInput < 0 && facingRight)) && !isFlipping)
        {
            StartCoroutine(DelayFlip(hInput));
        }

        if (hInput != 0)
        {
            animator.SetFloat("X", hInput);
        }

        return new float[] { hInput, vInput };
    }

    protected override IEnumerator MoveCharacter(Vector3 movement)
    {
        if (camera == null) camera = GameObject.FindWithTag("MainCamera");
        transform.position += movement;
        camera.transform.position += movement;
        yield return null;
    }

    private IEnumerator DelayFlip(float hInput)
    {
        isFlipping = true;
        yield return new WaitForSeconds(flipDelay); // Wait for the specified delay

        // Flip character direction after delay
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        isFlipping = false;
    }

}

