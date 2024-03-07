namespace Entity.Interfaces
{
    using UnityEngine;

    public interface IDamageable
    {
        bool IsAlive { get; }
        float MaxHealth { get; }
        float Health { get; }
        void Damage(float damageAmount, Vector2 knockbackDirection, float knockbackForce);
        void Heal(float healAmount);
        void Die();
    }
}