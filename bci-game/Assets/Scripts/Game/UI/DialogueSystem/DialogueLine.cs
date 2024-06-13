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

        private void Awake()
        {
            dialogueBox = GetComponent<TextMeshProUGUI>();
            dialogueBox.text = "";
        }

        private void Start()
        {
            StartCoroutine(showDialogue(text, dialogueBox, sound));
        }
    }
}
