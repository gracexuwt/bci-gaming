namespace Entity.Player
{
    using UnityEngine;
    using Entity.Utils;

    public class PlayerAttack : CharacterMovementController
    {
        public Transform Fire;

        void Update() {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        void Shoot() {

        }
    }
}