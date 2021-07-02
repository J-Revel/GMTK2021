using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatElement : MonoBehaviour
{
    public bool incrementMax = true;
    public bool setMaxFromConfig = false;
    public string statName;

    void Start()
    {
        if(setMaxFromConfig)
        {
            foreach(var objective in GameLauncher.instance.config.objectives)
            {
                if(objective.statName == statName)
                    ScoreService.instance.SetStatMax(statName, objective.value);
            }
        }
        if(incrementMax)
            ScoreService.instance.IncrStatMax(statName);
    }

    public void IncrStat()
    {
        ScoreService.instance.IncrStat(statName);
    }
}
