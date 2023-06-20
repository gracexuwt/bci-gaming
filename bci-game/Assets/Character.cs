using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f; //all speed in units/s
    public float gravity = 8f; //gravity accelaration
    public float airPenalty = 0.75f; //reduction in left/right speed while midair
    public float jumpSpeed = 15f;
    public float jumpDuration = 0.75f; //seconds

    private int[] movementBlocked = {0, 0, 0, 0}; //up, down, right, left
    private bool midair = false;
    private float fallTime = 0; //tracks time spent in the air
    private float jumpTime = 0; //tracks time since last jump
    private IDictionary<string, int> pastCollisions = new Dictionary<string, int>();

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
        float hChange = UpdateHorizontal(inputs[0]) * Time.deltaTime;
        if (hChange > 0 && movementBlocked[2] > 0) hChange = 0;
        else if (hChange < 0 && movementBlocked[3] > 0) hChange = 0;

        //get vertical velocity and check
        float vChange = UpdateVertical() * Time.deltaTime;
        if (vChange > 0 && movementBlocked[0] > 0) vChange = 0;
        else if (vChange < 0 && movementBlocked[1] > 0) vChange = 0;

        //update position
        StartCoroutine(MoveCharacter(new Vector3 (hChange, vChange, 0)));

        if (Time.frameCount % 75 == 0)
        {
            Shoot(new Vector3(-1,0,0));
        }
    }

    float UpdateHorizontal(float hInput) {
        float hMvmt = hInput * moveSpeed;
        if (midair) hMvmt *= airPenalty;
        return hMvmt;
    }

    float UpdateVertical() {
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

    IEnumerator MoveCharacter(Vector3 movement){
        transform.position += movement;
        yield return null;
    }

    //entering collision: block movement in collision direction
    void OnTriggerEnter(Collider other) {

        //check collisions by examining each corner of character and other object
        int collisionDirection = CheckCollisionDirection(transform, other.gameObject.transform); //0 = top, 1 = bottom, 2 = right, 3 = left;

        //contact above, below, left, and right
        movementBlocked[collisionDirection] += 1;

        //landing on ground
        if (collisionDirection == 1) {
            midair = false;
            fallTime = 0;
        }

        //hitting ceiling
        else if (collisionDirection == 0) {
            jumpTime = 0;
        }

        RemoveOverlap(collisionDirection, transform, other.gameObject.transform);

        //store collision info for exit function
        pastCollisions.Add(other.name, collisionDirection);
    }

    //exiting collision: restore movement
    void OnTriggerExit(Collider other) {
        int direction = pastCollisions[other.name];
        movementBlocked[direction] -= 1;
        pastCollisions.Remove(other.name);

        //walking off ledge
        if (direction == 1) {
            midair = true;
            if (jumpTime <= 0) fallTime = 0.5f;
            else fallTime = 0;
        }
    }

    private int CheckCollisionDirection(Transform self, Transform other) {
        //0 = top, 1 = bottom, 2 = right, 3 = left

        //corner order: top right, top left, bottom right, bottom left
        Vector3[] selfCorners = {
            new Vector3(self.transform.position.x + self.transform.localScale.x / 2, self.transform.position.y + self.transform.localScale.y / 2, 0),
            new Vector3(self.transform.position.x - self.transform.localScale.x / 2, self.transform.position.y + self.transform.localScale.y / 2, 0),
            new Vector3(self.transform.position.x + self.transform.localScale.x / 2, self.transform.position.y - self.transform.localScale.y / 2, 0),
            new Vector3(self.transform.position.x - self.transform.localScale.x / 2, self.transform.position.y - self.transform.localScale.y / 2, 0)
        };
        Vector3[] otherCorners = {
            new Vector3(other.transform.position.x + other.transform.localScale.x / 2, other.transform.position.y + other.transform.localScale.y / 2, 0),
            new Vector3(other.transform.position.x - other.transform.localScale.x / 2, other.transform.position.y + other.transform.localScale.y / 2, 0),
            new Vector3(other.transform.position.x + other.transform.localScale.x / 2, other.transform.position.y - other.transform.localScale.y / 2, 0),
            new Vector3(other.transform.position.x - other.transform.localScale.x / 2, other.transform.position.y - other.transform.localScale.y / 2, 0)
        };

        //check if each corner is within other object
        bool[] collisionCorners = new bool[8];
        
        //self top right
        collisionCorners[0] = CompareCornerNW(otherCorners[3], selfCorners[0], otherCorners[0]);
        //self top left
        collisionCorners[1] = CompareCornerSW(otherCorners[2], selfCorners[1], otherCorners[1]);
        //self bottom right
        collisionCorners[2] = CompareCornerSW(otherCorners[2], selfCorners[2], otherCorners[1]);
        //self bottom left
        collisionCorners[3] = CompareCornerNW(otherCorners[3], selfCorners[3], otherCorners[0]);
        //other top right
        collisionCorners[4] = CompareCornerNW(selfCorners[3], otherCorners[0], selfCorners[0]);
        //self top left
        collisionCorners[5] = CompareCornerSW(selfCorners[2], otherCorners[1], selfCorners[1]);
        //self bottom right
        collisionCorners[6] = CompareCornerSW(selfCorners[2], otherCorners[2], selfCorners[1]);
        //other bottom left
        collisionCorners[7] = CompareCornerNW(selfCorners[3], otherCorners[3], selfCorners[0]);
        // for (int i = 0; i < 8; i++) if (collisionCorners[i]) Debug.Log(i);
       
        //check collision direction
        if (collisionCorners[0] && collisionCorners[1]) return 0;
        else if (collisionCorners[2] && collisionCorners[3]) return 1;
        else if (collisionCorners[0] && collisionCorners[2]) return 2;
        else if (collisionCorners[1] && collisionCorners[3]) return 3;
        else if (collisionCorners[0]) {
            Vector3 diff = selfCorners[0] - otherCorners[3];
            if (diff.x > diff.y) return 0;
            else return 2;
        }
        else if (collisionCorners[1]) {
            Vector3 diff = selfCorners[1] - otherCorners[2];
            if (-diff.x > diff.y) return 0;
            else return 3;
        }
        else if (collisionCorners[2]) {
            Vector3 diff = otherCorners[0] - selfCorners[2];
            if (diff.x > diff.y) return 1;
            else return 3;
        }
        else if (collisionCorners[3]) {
            Vector3 diff = otherCorners[1] - selfCorners[3];
            if (-diff.x > diff.y) return 1;
            else return 2;
        }

        if (collisionCorners[6] && collisionCorners[7]) return 0;
        else if (collisionCorners[4] && collisionCorners[5]) return 1;
        else if (collisionCorners[5] && collisionCorners[7]) return 2;
        else if (collisionCorners[4] && collisionCorners[6]) return 3;

        return -1;
    }

    //check if mid is between low and high in NW direction
    private bool CompareCornerNW(Vector3 low, Vector3 mid, Vector3 high) {
        Vector3 v1 = mid - low;
        Vector3 v2 = high - mid;
        return (v1.x >= 0 && v1.y >= 0 && v2.x >= 0 && v2.y >= 0);
    }

    //check if mid is between low and high in SW direction
    private bool CompareCornerSW(Vector3 low, Vector3 mid, Vector3 high) {
        Vector3 v1 = mid - low;
        Vector3 v2 = high - mid;
        return (v1.x <= 0 && v1.y >= 0 && v2.x <= 0 && v2.y >= 0);
    }

    //move character so it does not overlap with platform
    private void RemoveOverlap(int direction, Transform self, Transform other) {
        float overlap;
        Vector3 change = new Vector3(0, 0, 0);

        //calculate overlap of objects
        //top
        if (direction == 0) {
            overlap = self.transform.position.y + self.transform.localScale.y / 2 - (other.transform.position.y - other.transform.localScale.y / 2);
            if (overlap > 0) change.y -= overlap;
        }
        //bottom
        if (direction == 1) {
            overlap = other.transform.position.y + other.transform.localScale.y / 2 - (self.transform.position.y - self.transform.localScale.y / 2);
            if (overlap > 0) change.y += overlap;
        }
        //right
        if (direction == 2) {
            overlap = self.transform.position.x + self.transform.localScale.x / 2 - (other.transform.position.x - other.transform.localScale.x / 2);
            if (overlap > 0) change.x -= overlap;
        }
        //left
        if (direction == 3) {
            overlap = other.transform.position.x + other.transform.localScale.x / 2 - (self.transform.position.x - self.transform.localScale.x / 2);
            if (overlap > 0) change.x += overlap;
        }

        StartCoroutine(MoveCharacter(change));
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

    public void Shoot(Vector3 direction) {
        GameObject bullet = new GameObject();
        bullet.name = "go1";
        bullet.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx"); // Use built-in sphere mesh
        bullet.AddComponent<MeshRenderer>(); // Add mesh renderer to render the mesh
        Projectile proj = bullet.AddComponent<Projectile>() as Projectile;
        SphereCollider sphereCollider = bullet.AddComponent<SphereCollider>();
        Rigidbody rigidbody = bullet.AddComponent<Rigidbody>();

        bullet.transform.position = transform.position + new Vector3(-2, 0, 0);
        bullet.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        proj.direction = direction;
        sphereCollider.isTrigger = true;
        rigidbody.useGravity = false;
    }

    


}
