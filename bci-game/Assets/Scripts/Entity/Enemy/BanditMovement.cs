namespace Entity.Enemy
{
    using UnityEngine;
    using Entity.Interfaces;
    using Entity.Utils;
    using Entity.Player;
    
    public class BanditMovement : CharacterMovementController
    {
        private IPositionTrackable playerPositionTracker;
        private Vector2 playerPosition;
        
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
            playerPositionTracker = FindObjectOfType<Player>() ?? throw new MissingReferenceException("Player not found");
        }

        protected override void Update()
        {
            base.Update();

            playerPosition = playerPositionTracker.GetPosition();
            
            // Animations and sound
            animator.SetFloat(Facing, -(body.velocity.x + 0.5f));
        }

        protected override Vector2 GetMovementInput()
        {
            if (Mathf.Abs(playerPosition.x - transform.position.x) < 8f)
            {
                return playerPosition.x < transform.position.x ? new Vector2 (-1f, 0) : new Vector2(1f, 0);
            }

            return new Vector2(0, 0);
        }
    }
}