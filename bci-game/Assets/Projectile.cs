using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 20f; // Speed at which the object moves
    public Vector3 direction = new Vector3(); // Direction of movement
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // destroy bullet when it has been flying for too long
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position += direction * speed * Time.deltaTime;

        // OnCollisionEnter(Collider other)
    }

    void OnTriggerEnter(Collider other) {
        // Destroy the game object if it collides with any other game object
        Player player = other.GetComponent<Player>();
        Projectile proj = other.GetComponent<Projectile>();
        if (player == null & proj == null)
        {
            Destroy(gameObject);
        }
    }
}
