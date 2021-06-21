using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChange : MonoBehaviour
{
    public int bonus = 100;
    public Stat statToIncrement;

    void Start()
    {
        ScoreService.instance.currentScore += bonus;
        ScoreService.instance.IncrStat(statToIncrement);
    }
}
