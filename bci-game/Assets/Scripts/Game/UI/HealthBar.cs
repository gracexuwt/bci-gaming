namespace Game.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Entity.Player;
    
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthFiller;

        private float maxHealth;
        private Player player;
        
        private void Start()
        {
            player = FindObjectOfType<Player>();
            maxHealth = player.MaxHealth;
        }

        private void Update()
        {
            healthFiller.fillAmount = Mathf.Clamp(player.Health / maxHealth, 0, 1);
        }
    }
}