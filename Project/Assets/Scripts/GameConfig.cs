using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveCondition 
{
    Equal, Greater, Lower, GreaterOrEqual, LowerOrEqual
}

[System.Serializable]
public struct StatObjective
{
    public string statName;
    public int startValue;
    public int maxValue;
    public bool isDefeatCondition;

    public bool countElements;
}

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    public int levelIndex;
    public StatObjective[] objectives;

    public StatObjective GetObjective(string statName)
    {
        foreach(var objective in objectives)
        {
            if(objective.statName == statName)
            {
                return objective;
            }
        }
        return new StatObjective();
    }

    public bool isStatActive(string stat)
    {
        foreach(var objective in objectives)
        {
            if(objective.statName == stat)
            {
                return true;
            }
        }
        return false;
    }
}
