using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSoundController : MonoBehaviour
{
    // Audio source attached to character self
    private AudioSource _audioSource;

    // Timers and intervals of repeated audio clips
    public float footstepInterval = 0.4f;
    private float _footstepTimer;
    
    // Audio clips for character
    public AudioClip[] footstepSounds;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip meleeAttackSound;
    public AudioClip rangeAttackSound;
    public AudioClip pickupCoinSound;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Increment footstep timer
        _footstepTimer += Time.deltaTime;
    }

    /**
     * Character movement SFX
     */
    public void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;
        if (_footstepTimer < footstepInterval) return;

        // Reset footstep timer
        _footstepTimer = 0f;
            
        AudioClip randomFootstep = footstepSounds[Random.Range(0, footstepSounds.Length)];
        _audioSource.PlayOneShot(randomFootstep);
    }
    
    public void PlayJumpSound()
    {
        _audioSource.PlayOneShot(jumpSound, 0.5f);
    }
    
    public void PlayLandSound()
    {
        _audioSource.PlayOneShot(landSound);
    }
    
    /**
     * Character combat SFX
     */
    public void PlayHurtSound()
    {
        _audioSource.PlayOneShot(hurtSound);
    }
    
    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(deathSound);
    }

    public void PlayMeleeAttackSound()
    {
        _audioSource.PlayOneShot(meleeAttackSound);
    }
    
    public void PlayRangeAttackSound()
    {
        _audioSource.PlayOneShot(rangeAttackSound);
    }

    /**
     * Character interaction SFX
     */
    public void PlayPickupCoinSound()
    {
        _audioSource.PlayOneShot(pickupCoinSound);
    }
}
