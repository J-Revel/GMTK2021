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
    public bool isFx = false;
    public bool isTimer = false;

    public Color _color;
    public Color color
    {
        get { return _color; }
        set { _color = value; UpdateDisplay(); }
    }
    public bool updateEachFrame;

    private void Start()
    {
        uiText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text = GetComponentInChildren<TMPro.TextMeshPro>();
        UpdateDisplay();
    }


    void Update()
    {
        if(updateEachFrame)
            UpdateDisplay();
    }

    void UpdateDisplay()
    {
        string str = prefix + ScoreService.instance.GetStat(stat).value + "/" + ScoreService.instance.GetStat(stat).max;
        if(GameLauncher.instance == null)
            return;
        if(GameLauncher.instance.config.GetObjective(stat).isDefeatCondition)
            str = "" + (ScoreService.instance.GetStat(stat).max - ScoreService.instance.GetStat(stat).value);
        if(isTimer)
        {
            int seconds = ScoreService.instance.GetStat(stat).max - ScoreService.instance.GetStat(stat).value;
            str = "" + Mathf.FloorToInt(seconds / 60).ToString("00") + ":" + (seconds % 60).ToString("00");
        }
        
        if(text != null)
        {
            text.text = str;
            text.color = color;
        }
        if(uiText != null)
        {
            uiText.text = str;
            uiText.color = color;

        }
    }
}
