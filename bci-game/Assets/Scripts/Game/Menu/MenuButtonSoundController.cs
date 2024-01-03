namespace Game.Menu
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class MenuButtonSoundController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Audio Source")]
        [SerializeField] private AudioSource audioSource;
    
        [Header("Audio Clips")]
        [SerializeField] private AudioClip selectSound;
        [SerializeField] private AudioClip deselectSound;
        [SerializeField] private AudioClip clickSound;
    
        [Header("Volume")]
        [SerializeField, Range(0f, 1f)] public float selectVolume = 0.8f;
        [SerializeField, Range(0f, 1f)] public float deselectVolume = 0.8f;
        [SerializeField, Range(0f, 1f)] public float clickVolume = 0.8f;
    
        // Button selected
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (selectSound == null) return;
            audioSource.PlayOneShot(selectSound, selectVolume);
        }
    
        // Button deselected
        public void OnPointerExit(PointerEventData eventData)
        {
            if (deselectSound == null) return;
            audioSource.PlayOneShot(deselectSound, deselectVolume);
        }
    
        // Button clicked
        public void OnPointerClick(PointerEventData eventData)
        {
            if (clickSound == null) return;
            audioSource.PlayOneShot(clickSound, clickVolume);
        }
    }
}