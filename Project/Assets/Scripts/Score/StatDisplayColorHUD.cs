using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDisplayColorHUD : MonoBehaviour
{
    private StatDisplay statDisplay;
    public Color defeatColor;
    public Color successColor;
    public Color defaultColor;
    private Color previousColor;
    void Start()
    {
        statDisplay = GetComponent<StatDisplay>();
    }

    void Update()
    {
        Color textColor;
        StatObjective objective = GameLauncher.instance.config.GetObjective(statDisplay.stat);
        if(objective.isDefeatCondition)
        {
            textColor = defeatColor;
        }
        else if(ScoreService.instance.GetStat(statDisplay.stat).value >= ScoreService.instance.GetStat(statDisplay.stat).max)
        {
            textColor = successColor;
        }
        else
        {
            textColor = defaultColor;
        }
        if(textColor != previousColor)
        {
            statDisplay.color = textColor;
            previousColor = textColor;
        }
    }
}
