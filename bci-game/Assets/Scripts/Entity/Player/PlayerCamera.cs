namespace Entity.Player
{
    using UnityEngine;
    using Entity.Interfaces;

    public class PlayerCamera : MonoBehaviour
    {
        [Header("Constant Vertical")]
        [SerializeField] private bool isYConstant = false;
        
        [Header("Camera Offset")]
        [SerializeField, Range(-5f, 5f)] private float xOffset = 0f;
        [SerializeField, Range(-5f, 5f)] private float yOffset = 0f;
        [SerializeField, Range(-15f, -1f)] private float zOffset = -10f;
        
        private IPositionTrackable playerPositionTracker;

        private float yConstant;
        private Vector3 finalPos;
        private Vector3 offset;

        private void Awake()
        {
            playerPositionTracker = FindObjectOfType<Player>();
            offset = new Vector3(xOffset, yOffset, zOffset);
        }

        private void Start()
        {
            yConstant = playerPositionTracker.GetPosition().y;
        }

        private void Update()
        {
            offset.x = xOffset;
            offset.y = yOffset;
            offset.z = zOffset;
            
            finalPos = (Vector3) playerPositionTracker.GetPosition() + offset;
            
            if (isYConstant)
                finalPos.y = yConstant + yOffset;
            
            transform.position = finalPos;
        }
    }
}
