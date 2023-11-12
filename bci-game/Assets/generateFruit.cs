using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateFruit : MonoBehaviour
{
    [SerializeField]
    public Sprite[] spriteList;
    [SerializeField]
    public GameObject[] platforms;
    // Start is called before the first frame update
    void Start()
    {
        spriteList = Resources.LoadAll<Sprite>("Fruits");
        platforms = GameObject.FindGameObjectsWithTag("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        // if (fruits.Length < 3)
        //    StartCoroutine(ReplaceFruits(fruits.Length));

        foreach (var fruit in fruits)
        {
            if (fruit.GetComponent<healAnim>() == null)
                fruit.AddComponent<healAnim>();
        }
    }

    /*IEnumerator ReplaceFruits(int fruits)
    {
        yield return new WaitForSeconds(4);
        while (fruits < 3)
        {
            fruits = GameObject.FindGameObjectsWithTag("Fruit").Length;
            Vector3 fruitPosition = new Vector3(0, 0, 0);
            var gameObject = new GameObject();
            foreach (var p in platforms)
            {
                Collider[] colliders = Physics.OverlapSphere(p.transform.position, 1f);
                bool fruitPresent = false;
                fruitPosition = p.transform.position;
                fruitPosition.y += 1f;
                foreach (var collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Fruit") && !collider.gameObject.CompareTag("Player")) fruitPresent = true;
                }

                if (!fruitPresent)
                {
                    break;
                }
            }
            gameObject.transform.position = fruitPosition;
            gameObject.name = "Fruit " + (fruits + 1);
            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            var boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            var rigidBody = gameObject.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
            var sprite = spriteList[Random.Range(0, spriteList.Length)];
            spriteRenderer.sprite = sprite;
        }
    }*/
}
