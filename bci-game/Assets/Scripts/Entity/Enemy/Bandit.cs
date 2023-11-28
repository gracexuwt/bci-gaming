namespace Entity.Enemy
{
    using UnityEngine;

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