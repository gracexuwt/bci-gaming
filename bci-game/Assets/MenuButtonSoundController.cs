using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonSoundController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Audio source attached to menu
    public AudioSource audioSource;
    
    // Audio clips for menu buttons
    public AudioClip selectSound;
    public AudioClip deselectSound;
    public AudioClip clickSound;
    
    // Button selected
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectSound == null) return;
        audioSource.PlayOneShot(selectSound, 0.5f);
    }
    
    // Button deselected
    public void OnPointerExit(PointerEventData eventData)
    {
        if (deselectSound == null) return;
        audioSource.PlayOneShot(deselectSound, 0.5f);
    }
    
    // Button clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound == null) return;
        audioSource.PlayOneShot(clickSound, 1.0f);
    }
}