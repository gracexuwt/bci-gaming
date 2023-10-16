using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : Player
{
    public float health;
    public float maxHealth;
    public Image filling;
    public Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        filling.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health / maxHealth <= 0 && isAlive)
        {
            isAlive = false;
            animator.SetTrigger("isDead");
            StartCoroutine(LoadSceneAfterDelay("DeathSeq", 1.0f));
        }
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}

