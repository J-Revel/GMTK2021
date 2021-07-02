using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{

    public string stat;
    public int multiplier;
    public string prefix;
    private TMPro.TextMeshProUGUI uiText;
    private TMPro.TextMeshPro text;
    public bool isDefeatCondition = false;
    public Color defaultColor;
    public Color successColor;
    public Color defeatColor;

    private void Start()
    {
        uiText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text = GetComponentInChildren<TMPro.TextMeshPro>();
    }

    void Update()
    {
        string str = prefix + ScoreService.instance.GetStat(stat).value + "/" + ScoreService.instance.GetStat(stat).max;
        if(GameLauncher.instance.config.GetObjective(stat).isDefeatCondition)
            str = "" + (ScoreService.instance.GetStat(stat).max - ScoreService.instance.GetStat(stat).value);

        Color textColor;
        StatObjective objective = GameLauncher.instance.config.GetObjective(stat);
        if(objective.isDefeatCondition)
        {
            textColor = defeatColor;
        }
        else if(ScoreService.instance.GetStat(stat).value >= ScoreService.instance.GetStat(stat).max)
        {
            textColor = successColor;
        }
        else
        {
            textColor = defaultColor;
        }
        if(text != null)
        {
            text.text = str;
            text.color = textColor;
        }
        if(uiText != null)
        {
            uiText.text = str;
            uiText.color = textColor;

        }
    }
}
