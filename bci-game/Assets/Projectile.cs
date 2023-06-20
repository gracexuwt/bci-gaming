using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 10f; // Speed at which the object moves
    public Vector3 direction = new Vector3(); // Direction of movement

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position += direction * speed * Time.deltaTime;

        // OnCollisionEnter(Collider other)
    }

    void OnTriggerEnter(Collider other) {
        // Destroy the game object if it collides with any other game object
        Destroy(gameObject);
    }
}
