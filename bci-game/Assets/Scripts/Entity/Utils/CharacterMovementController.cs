// Credits to Shinjingi for the base character controller code:
//    - (11/23) https://github.com/Shinjingi/Unity2D-Platform-Character-Controller

using UnityEngine;

namespace Entity.Utils
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterSoundController))]
    public abstract class CharacterMovementController : MonoBehaviour
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected CharacterSoundController soundController;
        
        [Header("Movement Parameters")]
        [SerializeField, Range(1f, 10f)] protected float maxSpeed = 6.0f;
        [SerializeField, Range(1f, 100f)] protected float maxAcceleration = 60f;
        [SerializeField, Range(0f, 100f)] protected float maxAirAcceleration = 30f;
        [SerializeField, Range(4f, 120f)] protected float jumpForce = 16.0f;
        [SerializeField, Range(1f, 10f)] protected float risingGravityScale = 3f;
        [SerializeField, Range(1f, 10f)] protected float fallingGravityScale = 6f;

        [Header("Ground Check")]
        [SerializeField, Range(0f, 90f)] private float maxNormalTilt = 60f;
        private ContactFilter2D groundFilter;

        protected bool onGround;
        protected bool isAlive = true;
        
        private Vector2 velocity;
        private Vector2 desiredVelocity;
        private float acceleration;
        
        private Vector2 input;
        private bool hasJumped; // Prevents double jumping on the same frame

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            soundController = GetComponent<CharacterSoundController>();
        
            // Setup ground check
            groundFilter.useLayerMask = true;
            groundFilter.useNormalAngle = true;
            groundFilter.layerMask = LayerMask.GetMask("Enemies", "Ground"); // Allow jumping on enemies and ground
            groundFilter.minNormalAngle = 90f - maxNormalTilt;
            groundFilter.maxNormalAngle = 90f + maxNormalTilt;
        }

        protected virtual void Update()
        {
            input = GetMovementInput();
            desiredVelocity = isAlive
                ? new Vector2(maxSpeed * input.x, input.y)
                : Vector2.zero;
        }

        private void FixedUpdate()
        {
            // Initialize current physics states
            onGround = CheckGrounded();
            body.gravityScale = SetGravity();

            
            // Start movement
            velocity = body.velocity;
            
            acceleration = onGround ? maxAcceleration : maxAirAcceleration;

            Walk();
            hasJumped = Jump();
            
            body.velocity = velocity;

            // Flip if switched directions (-1 is left, 1 is right)
            transform.localScale = body.velocity.x switch
            {
                < 0f => new Vector3(-1f, 1f, 1f),
                > 0f => new Vector3(1f, 1f, 1f),
                _ => transform.localScale
            };
        }

        private bool CheckGrounded()
        {
            return body.IsTouching(groundFilter);
        }

        private float SetGravity()
        {
            return body.velocity.y switch
            {
                > 0f => risingGravityScale,
                < 0f => fallingGravityScale,
                _ => body.gravityScale
            };
        }

        protected virtual void Walk()
        {
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, acceleration * Time.deltaTime);
        }
        
        /**
         * Returns: true if the character jumps successfully
         */
        protected virtual bool Jump()
        {
            if (hasJumped)
                return false;
            if (!onGround || desiredVelocity.y <= 0f) 
                return false;
            
            velocity.y = jumpForce;
            
            return true;
        }

        protected virtual Vector2 GetMovementInput()
        {
            return new Vector2(0f, 0f);
        }
    }
}

