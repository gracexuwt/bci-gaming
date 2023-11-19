using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateCameraBounds();
    }

    void CalculateCameraBounds()
    {
        if (mainCamera.orthographic)
        {
            float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float cameraHalfHeight = mainCamera.orthographicSize;

            minX = -cameraHalfWidth;
            maxX = cameraHalfWidth;
            minY = -cameraHalfHeight;
            maxY = cameraHalfHeight;
        }
        else
        {
            // Calculate bounds for perspective camera (not shown in this example)
            // You may need to use additional information like camera field of view, distance, etc.
        }
    }

    void Update()
    {
        // Clamp the player's position within camera bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Update player position
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
