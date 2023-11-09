using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSoundController : MonoBehaviour
{
    private AudioSource audioSource;

    private float footstepTimer;
    
    [Header("Timed Intervals")] 
    [SerializeField] private float footstepInterval = 0.4f;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip[] landSounds;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private AudioClip[] meleeAttackSounds;
    [SerializeField] private AudioClip[] rangeAttackSounds;
    [SerializeField] private AudioClip[] pickupCoinSounds;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Increment footstep timer
        footstepTimer += Time.deltaTime;
    }

    /**
     * Character movement SFX
     */
    public void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;
        if (footstepTimer < footstepInterval) return;

        // Reset footstep timer
        footstepTimer = 0f;
            
        AudioClip randomFootstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
        audioSource.PlayOneShot(randomFootstepSound);
    }
    
    public void PlayJumpSound()
    {
        if (jumpSounds.Length == 0) return;
        AudioClip randomJumpSound = jumpSounds[Random.Range(0, jumpSounds.Length)];
        audioSource.PlayOneShot(randomJumpSound, 0.5f);
    }
    
    public void PlayLandSound()
    {
        if (landSounds.Length == 0) return;
        AudioClip randomLandSound = landSounds[Random.Range(0, landSounds.Length)];
        audioSource.PlayOneShot(randomLandSound);
    }
    
    /**
     * Character combat SFX
     */
    public void PlayHurtSound()
    {
        if (hurtSounds.Length == 0) return;
        AudioClip randomHurtSound = hurtSounds[Random.Range(0, hurtSounds.Length)];
        audioSource.PlayOneShot(randomHurtSound);
    }
    
    public void PlayDeathSound()
    {
        if (deathSounds.Length == 0) return;
        AudioClip randomDeathSound = deathSounds[Random.Range(0, deathSounds.Length)];
        audioSource.PlayOneShot(randomDeathSound);
    }

    public void PlayMeleeAttackSound()
    {
        if (meleeAttackSounds.Length == 0) return;
        AudioClip randomMeleeAttackSound = meleeAttackSounds[Random.Range(0, meleeAttackSounds.Length)];
        audioSource.PlayOneShot(randomMeleeAttackSound);
    }
    
    public void PlayRangeAttackSound()
    {
        if (rangeAttackSounds.Length == 0) return;
        AudioClip randomRangeAttackSound = rangeAttackSounds[Random.Range(0, rangeAttackSounds.Length)];
        audioSource.PlayOneShot(randomRangeAttackSound);
    }

    /**
     * Character interaction SFX
     */
    public void PlayPickupCoinSound()
    {
        if (pickupCoinSounds.Length == 0) return;
        AudioClip randomPickupCoinSound = pickupCoinSounds[Random.Range(0, pickupCoinSounds.Length)];
        audioSource.PlayOneShot(randomPickupCoinSound);
    }
}
