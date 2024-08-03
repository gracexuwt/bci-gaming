using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace DialogueSystem 
{
    public class DialogueLine : Dialogue
    {
        private TextMeshProUGUI dialogueBox;
        [SerializeField] private string text;
        [SerializeField] private AudioClip sound;
        [SerializeField] private AudioClip introsound;
        [SerializeField] private GameObject image;
        [SerializeField] private bool isFirst = false;
        [SerializeField] private float waitTime = 0f; //used when isFirst is set to true

        private void Awake()
        {
            dialogueBox = GetComponent<TextMeshProUGUI>();
            dialogueBox.text = "";
        }

        private void Start()
        {
            if (introsound != null) DialogueSoundController.instance.playSound(introsound);
            StartCoroutine(showDialogue(text, dialogueBox, sound, image, isFirst, waitTime));
        }
    }
}
