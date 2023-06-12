using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{   
    public override float[] GetInput() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        return new float[] {hInput, vInput};
    }
}

