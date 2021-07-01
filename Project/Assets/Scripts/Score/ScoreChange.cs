using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChange : MonoBehaviour
{
    public string statToIncrement;

    void Start()
    {
        ScoreService.instance.IncrStat(statToIncrement);
    }
}
