using UnityEngine;

namespace Entity.Projectiles
{
    public class TemporaryProjectileEffect : MonoBehaviour
    {
        [SerializeField] private float effectDuration = 1f;
        
        private void Start()
        {
            Destroy(gameObject, effectDuration);
        }
    }
}