namespace Game.UI
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using Entity.Player;
    
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthFiller;
        [SerializeField] private Color healthColor;
        
        [Header("Damage indicator")]
        [SerializeField] private bool flashOnDamage = true;
        [SerializeField] private Color flashDamageColor = Color.white;
        [SerializeField] private float flashDamageDuration = 0.2f;
        
        [Header("Heal indicator")]
        [SerializeField] private bool flashOnHeal = false;
        [SerializeField] private Color flashHealColor = Color.white;
        [SerializeField] private float flashHealDuration = 0.2f;

        private float maxHealth;
        private float prevHealth;
        private Player player;
        
        private void Start()
        {
            player = FindObjectOfType<Player>();
            maxHealth = player.MaxHealth;
            prevHealth = maxHealth;

            healthFiller.color = healthColor;
        }

        private void Update()
        {
            float health = player.Health;
            healthFiller.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

            // On health change, flash the health bar
            if (Math.Abs(health - prevHealth) > 0.01f)
            {
                // Heal flashing
                if (health > prevHealth && flashOnHeal)
                    StartCoroutine(FlashHealthAnimation(flashHealColor, flashHealDuration));
                // Damage flashing
                else if (health < prevHealth && flashOnDamage)
                    StartCoroutine(FlashHealthAnimation(flashDamageColor, flashDamageDuration));
            }

            prevHealth = health;
        }
        
        private IEnumerator FlashHealthAnimation(Color color, float duration)
        {
            healthFiller.color = color;
            yield return new WaitForSeconds(0.05f);
            
            // Fade out effect
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                healthFiller.color = Color.Lerp(flashDamageColor, healthColor, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            healthFiller.color = healthColor;
            yield return null;
        }
    }
}