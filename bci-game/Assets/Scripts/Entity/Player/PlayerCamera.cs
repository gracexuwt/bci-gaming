namespace Entity.Player
{
    using UnityEngine;
    using Entity.Interfaces;

    public class PlayerCamera : MonoBehaviour
    {
        [Header("Camera Offset")]
        [SerializeField, Range(-5f, 5f)] public float xOffset = 0f;
        [SerializeField, Range(-5f, 5f)] public float yOffset = 0f;
        [SerializeField, Range(-15f, -1f)] public float zOffset = -10f;
        
        private IPositionTrackable playerPositionTracker;
    
        private Vector3 offset;

        private void Awake()
        {
            playerPositionTracker = FindObjectOfType<Player>();
            offset = new Vector3(xOffset, yOffset, zOffset);
        }

        private void Update()
        {
            offset.x = xOffset;
            offset.y = yOffset;
            offset.z = zOffset;
            transform.position = (Vector3) playerPositionTracker.GetPosition() + offset;
        }
    }
}
