using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public int displayValue = 0;
    public float deltaPerSecond = 50;
    private float animTime = 0;
    private TMPro.TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        animTime += Time.deltaTime;
        if(animTime > 1 / deltaPerSecond)
        {
            int iterationCount = Mathf.FloorToInt(animTime / (1 / deltaPerSecond));
            if(displayValue < ScoreService.instance.currentScore)
            {
                displayValue += iterationCount;
                if(displayValue > ScoreService.instance.currentScore)
                    displayValue = ScoreService.instance.currentScore;
            }
            else if(displayValue > ScoreService.instance.currentScore)
            {
                displayValue -= iterationCount;
                if(displayValue < ScoreService.instance.currentScore)
                    displayValue = ScoreService.instance.currentScore;
            }
            animTime -= iterationCount / deltaPerSecond;
        }
        text.text = "" + displayValue;
    }
}
