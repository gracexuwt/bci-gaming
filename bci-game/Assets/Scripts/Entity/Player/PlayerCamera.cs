using Entity.Interfaces;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Camera Offset")]
        [SerializeField] public float xOffset = 0f;
        [SerializeField] public float yOffset = 0f;
        [SerializeField] public float zOffset = -10f;
        
        private IPosition playerPosition;
    
        private Vector3 offset;

        private void Awake()
        {
            playerPosition = FindObjectOfType<Player>();
            offset = new Vector3(xOffset, yOffset, zOffset);
        }

        private void Update()
        {
            transform.position = (Vector3) playerPosition.GetPosition() + offset;
        }
    }
}
