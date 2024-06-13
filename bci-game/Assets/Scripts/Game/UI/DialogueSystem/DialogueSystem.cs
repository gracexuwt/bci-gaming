using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem 
{
    public class Dialogue : MonoBehaviour
    {
        // Steps to adding dialogue:
        //   1. Make a game object and add the Dialogue Controller script
        //   2. Under the Dialogue Controller, make DialogueLine game objects for dialogue

        public bool finished {get; private set;}

        /**
        * @brief Shows the dialogue text in TextMeshPro UI
        * @param input: text to be shown
        *        dialoguebox: TextMeshPro UI
        *        sound: to be played over dialogue
        */
        protected IEnumerator showDialogue(string input, TextMeshProUGUI dialogueBox, AudioClip sound) 
        {
            for(int i=0; i<input.Length; i++) 
            {
                dialogueBox.text += input[i];
                DialogueSoundController.instance.playSound(sound);
                yield return new WaitForSeconds(0.06f);
            }
            
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Return));
            finished = true;
        }
    }
}
