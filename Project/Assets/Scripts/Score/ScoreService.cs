using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat 
{
    Damage, Pee, Bin, Bird
};

public class ScoreService : MonoBehaviour
{
    public static ScoreService instance;
    public float gameTime = 0;
    public int currentScore = 0;
    public int[] stats = new int[sizeof(Stat)];
    public bool timerPaused = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(!timerPaused)
            gameTime += Time.deltaTime;
    }

    public void StartGame()
    {
        currentScore = 0;
        for(int i=0; i<stats.Length; i++)
        {
            stats[i] = 0;
        }
    }

    public void IncrStat(Stat stat)
    {
        stats[(int)stat]++;
    }
}
