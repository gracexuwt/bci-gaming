using Entity.Interfaces;
using UnityEngine;
using Entity.Utils;
using Entity.Player;

namespace Entity.Enemy
{
    public class BanditMovement : CharacterMovementController
    {
        private IPosition playerPosition;
        
        private void Start()
        {
            playerPosition =
                FindObjectOfType<Player.Player>(); // ?? throw new MissingReferenceException("Player not found");
        }

        protected override void Update()
        {
            base.Update();
            
            // animation/sound...
        }

        protected override Vector2 GetMovementInput() {
            return new Vector2(0, 0);
        }
    }
}