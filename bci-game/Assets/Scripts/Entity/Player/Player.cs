using Entity.Interfaces;
using UnityEngine;

namespace Entity.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour, IPosition
    {   
        private PlayerMovement playerMovement;
        
        public Vector2 GetPosition()
        {
            return transform.position;
        }
        
        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }
}