using UnityEngine;
using Entity.Interfaces;
using Entity.Utils;

namespace Entity.Enemies.Bandit
{
    using Entity.Player;
    
    public class BanditMovement : CharacterMovementController
    {
        private Bandit self;
        
        private IPositionTrackable playerPositionTracker;
        private Vector2 playerPosition;
        
        private static readonly int Facing = Animator.StringToHash("Bandit_X");

        private bool FIXED_STARTUP_MOVEMENT = false;

        private void Reset()
        {
            maxSpeed = 4f;
            maxAcceleration = 20f;
            maxAirAcceleration = 15f;
        }

        private void Start()
        {
            self = GetComponent<Bandit>();
            playerPositionTracker = FindFirstObjectByType<Player>();
        }

        protected override void Update()
        {
            if (!self.IsAlive) isAlive = false;

            // TODO: Temporary fix for startup movement
            // Bug still occurs occasionally when enemy is hit or stops moving, root issue not yet found
            if (!FIXED_STARTUP_MOVEMENT && onGround)
            {
                body.AddForce(new Vector2(0.1f, 0.1f), ForceMode2D.Impulse);
                FIXED_STARTUP_MOVEMENT = true;
            }
            
            base.Update();

            playerPosition = playerPositionTracker.GetPosition();
            
            // Animations and sound
        }

        protected override Vector2 GetMovementInput()
        {
            // Sample AI, can call an AI utils function instead
            float distanceToPlayer = Mathf.Abs(playerPosition.x - transform.position.x);
            if (distanceToPlayer is > 0.1f and < 8f)
            {
                return playerPosition.x < transform.position.x ? new Vector2 (-1f, 0f) : new Vector2(1f, 0f);
            }

            return new Vector2(0f, 0f);
        }
    }
}