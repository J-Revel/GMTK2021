using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : MonoBehaviour
{
    public static ScoreService instance;
    public float currentScore = 0;
    public int damageCount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void ResetScore()
    {
        currentScore = 0;
        damageCount = 0;
    }
}
