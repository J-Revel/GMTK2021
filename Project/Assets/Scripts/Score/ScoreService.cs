using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : MonoBehaviour
{
    public static ScoreService instance;
    public float gameTime = 0;
    public int currentScore = 0;
    public int damageCount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    private void StartGame()
    {
        currentScore = 0;
        damageCount = 0;
        gameTime = 0;
    }
}
