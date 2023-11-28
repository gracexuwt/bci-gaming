namespace Entity.Player
{
    using UnityEngine;
    using Entity.Interfaces;

    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour, IPositionTrackable
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