using System.Collections;
using UnityEngine;
using Entity.Interfaces;
using Entity.Utils;

namespace Entity.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator animator;
        private CharacterSoundController soundController;
        private Collider2D meleeAttackCollider;
        private Transform fire;
        
        [SerializeField] private float meleeAttackDuration = 0.5f;
        [SerializeField] private float damageAmount = 10f;
        [SerializeField] private float knockbackForce = 15f;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip[] meleeAttackSounds;
        [SerializeField, Range(0f, 1f)] private float meleeAttackVolume = 0.8f;

        private static readonly int Attack = Animator.StringToHash("isMelee");
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            soundController = GetComponent<CharacterSoundController>();
            meleeAttackCollider = transform.GetChild(0).GetComponent<Collider2D>();
        }

        private void Start()
        {
            meleeAttackCollider.enabled = false; // Disable collider until attack is initiated
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && !meleeAttackCollider.enabled) // Input && Able to attack
            {
                meleeAttackCollider.enabled = true;
                animator.SetTrigger(Attack);
                soundController.PlaySound(meleeAttackSounds, meleeAttackVolume);
                StartCoroutine(AttackCooldown(meleeAttackDuration));
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        // Basic attacks
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 7) // Enemy layer, can add other checks for layers, tags, specific obj, etc.
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable == null) return;
                
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                knockbackDirection.y += 0.4f;
                
                damageable.Damage(damageAmount, knockbackDirection, knockbackForce);
            }
        }

        private void Shoot()
        {
            
        }
        
        private IEnumerator AttackCooldown(float duration)
        {
            yield return new WaitForSeconds(duration);
            meleeAttackCollider.enabled = false;
        }
    }
}