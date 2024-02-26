namespace Entity.Collectables
{
    using UnityEngine;
    
    public abstract class TemplateFloatingObj : MonoBehaviour
    {
        [SerializeField] private float floatHeight = 0.3f;
        [SerializeField] private float floatSpeed = 1f;
        private Vector2 startPos;

        private void Start()
        {
            startPos = transform.position;
        }

        private void Update()
        {
            transform.position = new Vector2(
                transform.position.x, 
                startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight
            );
        }
    }
}