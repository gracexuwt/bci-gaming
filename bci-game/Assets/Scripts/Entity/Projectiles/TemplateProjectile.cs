using Entity.Interfaces;
using UnityEngine;

namespace Entity.Projectiles
{
    public abstract class TemplateProjectile : MonoBehaviour
    {
        [Header("Projectile Stats")]
        [SerializeField] private float projectileSpeed = 15f;
        [SerializeField] private float projectileDamage = 5f;
        [SerializeField] private float projectileKnockbackForce = 0f;
        [SerializeField] private bool affectedByGravity = false;
        [SerializeField] private float gravityScale = 1f;
        
        [Header("Projectile Life")]
        [SerializeField] private float projectileLifetime = 5f;
        [SerializeField] private LayerMask projectileCollisionMask;

        private Rigidbody2D body;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            // Initialize projectile velocity
            body.velocity = transform.right * projectileSpeed;
            
            body.gravityScale = affectedByGravity 
                ? gravityScale 
                : 0f;
            
            // Set a timer for the maximum projectile lifetime
            Destroy(gameObject, projectileLifetime);
        }
        
        private void FixedUpdate()
        {
            if (affectedByGravity)
            {
                // Rotate the projectile to face the direction it's moving
                transform.right = body.velocity;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // If the other object is in the projectileCollisionMask layer(s)
            if ((projectileCollisionMask & (1 << other.gameObject.layer)) != 0)
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                    knockbackDirection.y += 0.8f;

                    damageable.Damage(projectileDamage, knockbackDirection.normalized, projectileKnockbackForce);
                }

                Destroy(gameObject);
            }
        }
    }
}