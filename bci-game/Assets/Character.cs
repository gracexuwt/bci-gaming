using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 15f; //all speed in units/s
    public float gravity = 20f; //gravity accelaration
    public float airPenalty = 0.75f; //reduction in left/right speed while midair
    public float jumpSpeed = 40f;
    public float jumpDuration = 0.75f; //seconds

    private int[] movementBlocked = {0, 0, 0, 0}; //up, down, right, left
    private bool midair = false;
    private float fallTime = 0; //tracks time spent in the air
    private float jumpTime = 0; //tracks time since last jump

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //get movement inputs, LR used to determine horizontal movement, up to start jump
        float[] inputs = GetInput();
        if (!midair && inputs[1] > 0) Jump();
        
        //get horizontal velocity and check if movement is blocked
        float hChange = updateHorizontal(inputs[0]) * Time.deltaTime;
        if (hChange > 0 && movementBlocked[2] > 0) hChange = 0;
        else if (hChange < 0 && movementBlocked[3] > 0) hChange = 0;

        //get vertical velocity and check
        float vChange = updateVertical() * Time.deltaTime;
        if (vChange > 0 && movementBlocked[0] > 0) vChange = 0;
        else if (vChange < 0 && movementBlocked[1] > 0) vChange = 0;

        //update position
        transform.position += new Vector3(hChange, vChange, 0);
    }

    float updateHorizontal(float hInput) {
        float hMvmt = hInput * moveSpeed;
        if (midair) hMvmt *= airPenalty;
        return hMvmt;
    }

    float updateVertical() {
        float vMvmt = 0f;

        if (midair) {
            if (jumpTime > 0) {
                vMvmt += jumpSpeed * jumpTime;
                jumpTime -= Time.deltaTime;
            }
            //v = gt implementation
            fallTime += Time.deltaTime;
            vMvmt -= gravity * fallTime;
        }
        return vMvmt;

    }

    //collision work in progress
    void OnTriggerEnter(Collider other) {
        Vector3 contact = other.ClosestPoint(transform.position) - transform.position;
        // Debug.Log(contact);

        //contact above, below, left, and right
        if (contact.y >= 0.75f) movementBlocked[0] += 1;
        else if (contact.y <= -0.75f) movementBlocked[1] += 1;
        else if (contact.x >= 0.4f) movementBlocked[2] += 1;
        else if (contact.x <= -0.4f) movementBlocked[3] += 1;

        //set flag on landing
        if (contact.y <= -0.8f) {
            midair = false;
            fallTime = 0;
        }
    }

    //collision work in progress
    void OnTriggerExit(Collider other) {
        Vector3 contact = other.ClosestPoint(transform.position) - transform.position;
        // Debug.Log(contact);

        //contact above, below, left, and right
        if (contact.y >= 1f) movementBlocked[0] -= 1;
        else if (contact.y <= -1f) movementBlocked[1] -= 1;
        else if (contact.x >= 0.4f) movementBlocked[2] -= 1 ;
        else if (contact.x <= -0.4f) movementBlocked[3] -= 1;
    }

    public virtual float[] GetInput() {
        return new float[] {0, 0};
    }

    public bool Midair() {
        return midair;
    }

    public void Jump() {
        midair = true;
        jumpTime = jumpDuration;
    }


}
