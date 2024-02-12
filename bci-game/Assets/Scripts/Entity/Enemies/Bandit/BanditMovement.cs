namespace Entity.Enemies.Bandit
{
    using UnityEngine;
    using Entity.Interfaces;
    using Entity.Utils;
    using Entity.Player;
    
    public class BanditMovement : CharacterMovementController
    {
        private IPositionTrackable playerPositionTracker;
        private Vector2 playerPosition;

        private BoxCollider2D attackPoint;
        
        private static readonly int Facing = Animator.StringToHash("Bandit_X");
        private static readonly int Attack = Animator.StringToHash("BanditAttack");
        private static readonly int Die = Animator.StringToHash("BanditDead");

        private void Reset()
        {
            maxSpeed = 4f;
            maxAcceleration = 20f;
            maxAirAcceleration = 15f;
        }

        private void Start()
        {
            playerPositionTracker = FindFirstObjectByType<Player>();
            attackPoint = transform.GetChild(0).GetComponent<BoxCollider2D>();
        }

        protected override void Update()
        {
            base.Update();

            playerPosition = playerPositionTracker.GetPosition();
            
            // Animations and sound
        }

        protected override Vector2 GetMovementInput()
        {
            // Sample AI, can call an AI utils function instead
            float distanceToPlayer = Mathf.Abs(playerPosition.x - transform.position.x);
            if (0.1 < distanceToPlayer && distanceToPlayer < 8f)
            {
                return playerPosition.x < transform.position.x ? new Vector2 (-1f, 0) : new Vector2(1f, 0);
            }

            return new Vector2(0, 0);
        }
    }
}