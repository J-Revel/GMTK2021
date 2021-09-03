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
    public System.Action defeatDelegate;
    public UnityEngine.Events.UnityEvent defeatEvent;

    private void Awake()
    {
        instance = this;
        if(GameLauncher.instance == null)
            return;
        foreach(var objective in GameLauncher.instance.config.objectives)
        {
            stats[objective.statName] = new StatValue();
            stats[objective.statName].value = objective.startValue;
            stats[objective.statName].max = objective.maxValue;

        }
    }

    private void Update()
    {
        if(!timerPaused)
            gameTime += Time.deltaTime;
    }

    public void IncrStat(string statName)
    {
        if(!stats.ContainsKey(statName))
        {
            stats[statName] = new StatValue();
        }
        stats[statName].value++;
        CheckDefeat(statName);
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
        CheckDefeat(statName);
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

    private void CheckDefeat(string statName)
    {
        if(GameLauncher.instance == null)
            return;
        if(GameLauncher.instance.config.GetObjective(statName).isDefeatCondition && stats[statName].value > stats[statName].max)
        {
            defeatDelegate?.Invoke();
            defeatEvent.Invoke();
        }
    }
}
