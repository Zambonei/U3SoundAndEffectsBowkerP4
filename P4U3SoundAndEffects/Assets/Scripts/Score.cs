using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreZone"))
        {
            score++;
            Debug.Log("Score: " + score);
        }
    }
}
