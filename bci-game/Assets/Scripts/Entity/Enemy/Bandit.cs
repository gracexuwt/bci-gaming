using UnityEngine;

namespace Entity.Enemy
{
    [RequireComponent(typeof(BanditMovement))]
    public class Bandit : MonoBehaviour
    {
        private BanditMovement banditMovement;
        
        private void Awake()
        {
            banditMovement = GetComponent<BanditMovement>();
        }
    }
}