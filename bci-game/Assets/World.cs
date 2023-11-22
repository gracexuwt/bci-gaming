using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public enum EnemyType{
        Bandit,
        Cowboy,
        Cactus
    }
    private int numEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(EnemyType type, float xPos, float yPos){
        if (type == EnemyType.Bandit) {
            GameObject template = GameObject.Find("BanditTemp");
            GameObject newEnemy = Instantiate(template, new Vector3(xPos, yPos, 0), Quaternion.identity);
            newEnemy.name = "Bandit" + numEnemies.ToString();

            Character script = newEnemy.GetComponent<Character>();
            script.moveSpeed = 1;
            script.gravity = 8;
        }

        numEnemies += 1;
    }

    public void SpawnEnemyButton(string s){
        string[] info = s.Split(' ');
        EnemyType type = (EnemyType) int.Parse(info[0]);
        float xPos = float.Parse(info[1]);
        float yPos = float.Parse(info[2]);

        SpawnEnemy(type, xPos, yPos);
    }
}
