using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{   
    private GameObject camera;

    public override float[] GetInput() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        if (hInput != 0) {
            animator.SetFloat("X", hInput);
        }

        return new float[] {hInput, vInput};
    }

    protected override IEnumerator MoveCharacter(Vector3 movement){
        if (camera == null) camera = GameObject.FindWithTag("MainCamera");
        transform.position += movement;
        camera.transform.position += movement;
        yield return null;
    }

}

