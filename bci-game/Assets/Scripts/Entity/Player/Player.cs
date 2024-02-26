namespace Entity.Player
{
    using System.Collections;
    using UnityEngine;
    using Entity.Interfaces;
    using Entity.Utils;

    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(CharacterSoundController))]
    public class Player : MonoBehaviour, IPositionTrackable, IDamageable
    {
        private Rigidbody2D body;
        private Animator animator;
        private CharacterSoundController soundController;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip[] hurtSounds;
        [SerializeField, Range(0f, 1f)] private float hurtVolume = 0.8f;
        [SerializeField] private AudioClip[] healSounds;
        [SerializeField, Range(0f, 1f)] private float healVolume = 0.8f;
        [SerializeField] private AudioClip deathSound;
        [SerializeField, Range(0f, 1f)] private float deathVolume = 0.8f;
        
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsHurt = Animator.StringToHash("isHurt");

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            soundController = GetComponent<CharacterSoundController>();
            
            // Set health to max health on game start
            IsAlive = true;
            Health = MaxHealth;
        }
        
        // IPositionTrackable implementation
        public Vector2 GetPosition()
        {
            return transform.position;
        }

        // IDamageable implementation
        public bool IsAlive { get; private set; }
        public float MaxHealth => 100;
        public float Health { get; private set; }

        public void Damage(float damageAmount, Vector2 knockbackDirection, float knockbackForce)
        {
            if (Health <= 0) return; // Prevents damage after death
            
            Health -= damageAmount;
            if (Health <= 0) Die();
            
            soundController.PlaySound(hurtSounds, hurtVolume);
            animator.SetTrigger(IsHurt);

            Vector2 knockback = knockbackDirection.normalized * knockbackForce;
            body.AddForce(knockback, ForceMode2D.Impulse);
        }

        public void Heal(float healAmount)
        {
            Health = Mathf.Clamp(Health + healAmount, 0, MaxHealth);
            soundController.PlaySound(healSounds, healVolume);
        }
        
        public void Die()
        {
            soundController.PlaySound(deathSound, deathVolume);
            animator.SetTrigger(IsDead);
            
            float deathSoundLen = deathSound != null 
                ? deathSound.length 
                : 0;

            float deathAnimLen = animator.GetCurrentAnimatorClipInfo(0).Length > 0
                ? animator.GetCurrentAnimatorClipInfo(0)[0].clip.length
                : 0;

            IsAlive = false;
            
            EntityUtils.MarkForDeath(gameObject, Mathf.Max(deathSoundLen, deathAnimLen));
        }
    }
}