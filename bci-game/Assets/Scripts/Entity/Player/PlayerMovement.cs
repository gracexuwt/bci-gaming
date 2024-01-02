namespace Entity.Player
{
    using UnityEngine;
    using Entity.Utils;

    public class PlayerMovement : CharacterMovementController
    {
        [Space]
    
        [Header("Movement Buffers")]
        [SerializeField, Range(0, 10)] private int preJumpBuffer = 3;
        [SerializeField, Range(0, 10)] private int postJumpBuffer = 3;
        
        private static readonly int Facing = Animator.StringToHash("X");
        private static readonly int Walking = Animator.StringToHash("IsWalking");
        private static readonly int Jumping = Animator.StringToHash("isJumping");
        private static readonly int Takeoff = Animator.StringToHash("takeoff");

        private void Start()
        {
            animator.SetFloat(Facing, 1);
        }

        protected override void Update()
        {
            base.Update(); 
            
            // Animations and sound
            animator.SetBool(Walking, Mathf.Abs(body.velocity.x) > 0.2f);
            animator.SetBool(Jumping, Mathf.Abs(body.velocity.y) > 0.2f);
            
            if (onGround && Mathf.Abs(body.velocity.x) > 0.2f)
            {
                soundController.PlayFootstepSound();
            } 
        }

        protected override void Jump()
        {
            if (hasJumped)
            {
                hasJumped = false;
                return;
            }
            
            if (desiredVelocity.y > 0 && onGround)
            {
                velocity.y = jumpForce;           

                animator.SetTrigger(Takeoff);
                soundController.PlayJumpSound();
                hasJumped = true;
            }
        }

        protected override Vector2 GetMovementInput() {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
