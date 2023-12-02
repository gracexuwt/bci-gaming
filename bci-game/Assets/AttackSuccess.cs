using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSuccess : MonoBehaviour
{
    private bool successful(int successProbability)
    {
        float randomValue = Random.Range(0f, 100f);

        bool isSuccess = randomValue <= successProbability;

        return isSuccess;
    }

}
