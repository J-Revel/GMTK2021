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
        if(incrementMax && GameLauncher.instance.config.GetObjective(statName).countElements)
            ScoreService.instance.IncrStatMax(statName);
    }

    public void IncrStat()
    {
        ScoreService.instance.IncrStat(statName);
    }
}
