using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 5f;
    public float airPenalty = 0.5f;
    public float jumpSpeed = 5f;
    public float jumpDuration = 0.5f;
    private float fallTime = 0;
    private float jumpTime = 0;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        float horizMvmt = Input.GetAxis("Horizontal") * moveSpeed;

        //vertical movement
        float vertMvmt = 0;
        //mid-air falling
        if (!grounded) {
            if (jumpTime > 0) {
                vertMvmt += jumpSpeed * jumpTime;
                jumpTime -= Time.deltaTime;
            }
            horizMvmt *= airPenalty;
            fallTime += Time.deltaTime;
            vertMvmt -= gravity * fallTime;
        }
        //jump
        else if (grounded && Input.GetAxis("Vertical") > 0) {
            jumpTime = jumpDuration;
            grounded = false;
        }
            

        //update position
        transform.position += new Vector3(horizMvmt * Time.deltaTime, vertMvmt * Time.deltaTime, 0);
    }

    void OnTriggerEnter() {
        Debug.Log("bro");
        fallTime = 0;
        grounded = true;
    }

}
