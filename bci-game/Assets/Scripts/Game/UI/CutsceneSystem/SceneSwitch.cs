using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueSystem;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] private SequenceController controller;
    public Animator transition;

    public float transitionTime = 2f;

    void Update()
    {
        if(controller != null && controller.IsEnd())
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator Load(int index)
    {
        transition.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
