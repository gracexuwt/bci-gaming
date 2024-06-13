using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSoundController : MonoBehaviour
{
    public static DialogueSoundController instance; //Do not mutate

    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void playSound(AudioClip sound) 
    {
        source.PlayOneShot(sound);
    }
}
