using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{   
    public void TakeDamage(int damage)
{
    // TODO: Implement the logic for player taking damage
    Debug.Log("Player took damage: " + damage);
}

    public override float[] GetInput() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        return new float[] {hInput, vInput};
    }


}

