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
    [SerializeField, Range(0f, 1f)] private float selectVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float deselectVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float clickVolume = 1f;
    
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