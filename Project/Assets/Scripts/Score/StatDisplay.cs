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

    private void Start()
    {
        uiText = GetComponent<TMPro.TextMeshProUGUI>();
        text = GetComponent<TMPro.TextMeshPro>();
    }

    void Update()
    {
        if(text != null)
            text.text = prefix + ScoreService.instance.GetStat(stat).value + "/" + ScoreService.instance.GetStat(stat).max;
        if(uiText != null)
            uiText.text = prefix + ScoreService.instance.GetStat(stat).value + "/" + ScoreService.instance.GetStat(stat).max;
    }
}
