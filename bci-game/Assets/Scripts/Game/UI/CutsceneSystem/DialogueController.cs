using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private GameObject image;
        private bool finished = false;

        private void Awake()
        {
            StartCoroutine(dialogueSequence());
        }

        private IEnumerator dialogueSequence()
        {
            for(int i=0; i<transform.childCount; i++)
            {
                Deactivate(); //remove previous dialogue
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            //Close dialogue box
            gameObject.SetActive(false);
            image.SetActive(false);
            finished = true;
        }

        private void Deactivate()
        {
            for (int i=0; i<transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public bool IsFinished()
        {
            return finished;
        }
    }
}
