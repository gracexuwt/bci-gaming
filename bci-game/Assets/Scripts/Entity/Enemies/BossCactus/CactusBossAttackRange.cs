using System;
using System.Collections;
using Entity.Utils;
using UnityEngine;

namespace Entity.Enemies.BossCactus
{
    using Entity.Player;
    
    public class CactusBossAttackRange : MonoBehaviour
    {
        private Animator animator;
        private CharacterSoundController soundController;
        
        private Rigidbody2D player;
        private bool playerInRange;
        
        private bool canFire = true;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float suctionForce = 100f;
        [SerializeField] private float rangeAttackSpeed = 1f;
        [SerializeField] private float attackRange = 16f;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip[] rangeAttackSounds;
        [SerializeField, Range(0f, 1f)] private float rangeAttackVolume = 0.8f;

        private static readonly int Attack = Animator.StringToHash("Attack");


        private void Awake()
        {
            soundController = GetComponent<CharacterSoundController>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            player = FindFirstObjectByType<Player>().GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            playerInRange = CheckPlayerInRange();
            if (canFire && playerInRange)
                Shoot();
            
            Debug.Log(playerInRange);
        }
        
        private bool CheckPlayerInRange()
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.localScale.x < 0 ? Vector2.left : Vector2.right, attackRange, ~LayerMask.GetMask("Enemies"));
            // Debug.DrawRay(firePoint.position, transform.localScale.x < 0 ? Vector2.left : Vector2.right, Color.green);
            
            return hit && hit.collider.CompareTag("Player");
        }

        private void Shoot()
        {
            canFire = false;
            
            animator.SetTrigger(Attack);
            
            // shoot after animation finishes playing
            soundController.PlaySound(rangeAttackSounds, rangeAttackVolume);
            // suck in player
            Vector3 direction = firePoint.position - player.transform.position;
            player.AddForce(direction.normalized * suctionForce, ForceMode2D.Impulse);
            
            StartCoroutine(AttackCooldown(1f / rangeAttackSpeed));
        }

        private IEnumerator AttackCooldown(float duration)
        {
            yield return new WaitForSeconds(duration);
            canFire = true;
        }
    }
}