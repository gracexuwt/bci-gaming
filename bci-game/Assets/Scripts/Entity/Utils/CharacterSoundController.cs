namespace Entity.Utils
{
    using UnityEngine;
    using Random = UnityEngine.Random;

    [RequireComponent(typeof(AudioSource))]
    public class CharacterSoundController : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip, float volume)
        {
            audioSource.PlayOneShot(clip, volume);
        }

        public void PlaySound(AudioClip[] clips, float volume)
        {
            if (clips.Length == 0) return;
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
        }
    }
}
