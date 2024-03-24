namespace Game.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Entity.Enemies.Bandit;

    public class EnemyHealthBar : MonoBehaviour
    {
        private GameObject healthFillerMask;

        private float maxHealth;
        private Bandit bandit;

        private void Start()
        {
            healthFillerMask = transform.parent.Find("Mask").gameObject;
            bandit = FindObjectOfType<Bandit>();
            maxHealth = bandit.MaxHealth;
        }

        private void Update()
        {
            healthFillerMask.transform.localScale = new Vector3(bandit.Health / maxHealth, 1.0f, 0.0f);
        }
    }
}