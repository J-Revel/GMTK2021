using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDisplayColorFX : MonoBehaviour
{
    private StatDisplay statDisplay;
    public Color bonusColor;
    public Color malusColor;

    void Start()
    {
        statDisplay = GetComponent<StatDisplay>();
        StatObjective objective = GameLauncher.instance.config.GetObjective(statDisplay.stat);
        statDisplay.color = objective.isDefeatCondition ? malusColor : bonusColor;
    }

    void Update()
    {
    }
}
