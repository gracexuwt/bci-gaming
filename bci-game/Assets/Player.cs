using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{   
    

    public override object[] GetInput() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 shootTargetInput = GetMouseDirection();
        bool leftMouseInput = Input.GetMouseButton(0);
        return new object[] {hInput, vInput, shootTargetInput, leftMouseInput};
    }

    private Vector2 GetMouseDirection()
    {
        // Get mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        // print("mouse: " + Camera.main.ScreenToWorldPoint(mousePosition).ToString());
        // print("transform: " + transform.position.ToString());

        // Convert the mouse position to world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the z-axis to the GameObject's z-axis
        mousePosition.z = transform.position.z;

        // Calculate direction from gameObject to mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        return direction;
    }


}

