using Entity.Interfaces;
using Entity.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity.Player
{
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

        protected override void Update()
        {
            base.Update(); 
            
            animator.SetFloat(Facing, body.velocity.x + 0.50F);
            animator.SetBool(Walking, body.velocity.x != 0f);
            animator.SetBool(Jumping, Mathf.Abs(body.velocity.y) > 0.2f);
            
            if (onGround && Mathf.Abs(body.velocity.x) > 0.2f)
            {
                soundController.PlayFootstepSound();
            } 
        }
        
        protected override void Jump()
        {
            base.Jump();
            animator.SetTrigger(Takeoff);
            soundController.PlayJumpSound();
            
            // TODO: Add jump buffer
        }

        protected override Vector2 GetMovementInput() {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
