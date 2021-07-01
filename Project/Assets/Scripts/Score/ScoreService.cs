using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat 
{
    Damage, Pee, Bin, Bird
};

public class StatValue
{
    public int value;
    public int max;
}

public class ScoreService : MonoBehaviour
{
    public static ScoreService instance;
    public float gameTime = 0;
    public int currentScore = 0;
    public Dictionary<string, StatValue> stats = new Dictionary<string, StatValue>();
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
        
    }

    public void IncrStat(string statName)
    {
        if(!stats.ContainsKey(statName))
        {
            stats[statName] = new StatValue();
        }
        stats[statName].value++;
    }

    public void DecrStat(string statName)
    {
        if(!stats.ContainsKey(statName))
        {
            stats[statName] = new StatValue();
        }
        stats[statName].value--;
    }

    public void SetStat(string statName, int value)
    {
        if(!stats.ContainsKey(statName))
        {
            stats[statName] = new StatValue();
        }
        stats[statName].value = value;
    }

    public void IncrStatMax(string statName)
    {
        if(!stats.ContainsKey(statName))
        {
            stats[statName] = new StatValue();
        }
        stats[statName].max++;
    }

    public void SetStatMax(string statName, int max)
    {
        if(!stats.ContainsKey(statName))
        {
            stats[statName] = new StatValue();
        }
        stats[statName].max = max;
    }

    public StatValue GetStat(string statName)
    {
        if(!stats.ContainsKey(statName))
        {
            stats[statName] = new StatValue();
        }
        return stats[statName];
    }
}
