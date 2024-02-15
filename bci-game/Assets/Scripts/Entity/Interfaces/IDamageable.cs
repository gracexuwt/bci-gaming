namespace Entity.Interfaces
{
    using UnityEngine;

    public interface IDamageable
    {
        // float Health { get; set; }
        // void Die();
        void Damage(float damageAmount, Vector2 knockbackDirection, float knockbackForce);
    }
}