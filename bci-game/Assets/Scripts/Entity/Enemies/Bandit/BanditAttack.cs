namespace Entity.Enemies.Bandit
{
    using UnityEngine;
    using Entity.Interfaces;
    
    public class BanditAttack : MonoBehaviour
    {
        [SerializeField] private float damageAmount = 10f;
        [SerializeField] private float knockbackForce = 7.5f;

        // Basic attacks
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable == null) return;
                
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                knockbackDirection.y += 0.4f;
                
                damageable.Damage(damageAmount, knockbackDirection, knockbackForce);
            }
        }
    }
}