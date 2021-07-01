using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatElement : MonoBehaviour
{
    public string statName;
    void Start()
    {
        ScoreService.instance.IncrStatMax(statName);
    }

    public void IncrStat()
    {
        ScoreService.instance.IncrStat(statName);
    }
}
