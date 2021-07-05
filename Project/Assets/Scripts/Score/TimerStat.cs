using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStat : MonoBehaviour
{
    public string statName = "timer";
    private float time = 0;
    void Start()
    {
        
    }

    public void IncrStat()
    {
        ScoreService.instance.IncrStat(statName);
    }

    void Update()
    {
        time += Time.deltaTime;
        ScoreService.instance.SetStat(statName, Mathf.FloorToInt(time));
    }
}
