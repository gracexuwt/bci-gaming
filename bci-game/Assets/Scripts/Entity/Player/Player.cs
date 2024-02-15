namespace Entity.Player
{
    using UnityEngine;
    using Entity.Interfaces;
    using Entity.Utils;

    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(CharacterSoundController))]
    public class Player : MonoBehaviour, IPositionTrackable, IDamageable
    {
        private Rigidbody2D body;
        private CharacterSoundController soundController;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip[] hurtSounds;
        [SerializeField, Range(0f, 1f)] private float hurtVolume = 0.8f;
        
        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            soundController = GetComponent<CharacterSoundController>();
        }
        
        // IPositionTrackable implementation
        public Vector2 GetPosition()
        {
            return transform.position;
        }

        // IDamageable implementation
        public void Damage(float damageAmount, Vector2 knockbackDirection, float knockbackForce)
        {
            // health.health -= damageAmount;
            // if(health.health < 0) Die();
            soundController.PlaySound(hurtSounds, hurtVolume);

            Vector2 knockback = knockbackDirection.normalized * knockbackForce;
            body.AddForce(knockback, ForceMode2D.Impulse);
        }
    }
}