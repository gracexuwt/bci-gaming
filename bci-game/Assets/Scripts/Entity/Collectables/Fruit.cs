namespace Entity.Collectables
{
    using UnityEngine;
    using Entity.Player;
    using Entity.Utils;

    public class Fruit : TemplateFloatingObj
    {
        [Header("Sounds")]
        [SerializeField] private AudioClip[] collectSounds;
        [SerializeField, Range(0f, 1f)] private float collectVolume = 0.8f;

        private void OnTriggerEnter2D(Collider2D collector)
        {
            Player player = collector.GetComponent<Player>();
            if (player != null)
            {
                player.GetComponent<CharacterSoundController>().PlaySound(collectSounds, collectVolume);
                // play anim
                
                player.Heal(10);
                EntityUtils.MarkForDeath(gameObject, 0, false);
            }
        }
    }
}