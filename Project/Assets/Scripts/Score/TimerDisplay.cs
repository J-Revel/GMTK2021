using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = (ScoreService.instance.gameTime / 60).ToString("00") + ":" + (ScoreService.instance.gameTime % 60).ToString("00");
    }
}
