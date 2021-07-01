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
    public int value;
    public ObjectiveCondition condition;
}

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    public int levelIndex;
    public StatObjective[] objectives;
}
