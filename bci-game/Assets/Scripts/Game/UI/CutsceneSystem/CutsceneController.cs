using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class SequenceController : MonoBehaviour
    {
        [SerializeField] private GameObject[] dialogueControllers;
        [SerializeField] private Animator animator;
        [SerializeField] private string[] animations; //animations to play, all triggers
        private bool end = false;

        private void Start()
        {
            StartCoroutine(PlayCutscenes());
        }

        private IEnumerator PlayCutscenes()
        {
            for (int i = 0; i < dialogueControllers.Length; i++)
            {
                //Play Dialogue
                GameObject dialogueController = dialogueControllers[i];
                dialogueController.SetActive(true);
                yield return new WaitUntil(() => dialogueController.GetComponent<DialogueController>().IsFinished());

                //Play animations (If set to null, then no animations would be played after dialogue)
                if (i < animations.Length && !string.IsNullOrEmpty(animations[i]))
                {
                    if(i > 0 && !string.IsNullOrEmpty(animations[i-1]))
                    {
                        animator.SetTrigger(animations[i-1]);
                    }
                    animator.SetTrigger(animations[i]);
                }
            }
            end = true;
        }

        public bool IsEnd()
        {
            return end;
        }
    }
}
