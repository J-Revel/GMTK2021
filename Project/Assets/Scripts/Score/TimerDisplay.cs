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
        int time = Mathf.Max(0, Mathf.FloorToInt(ScoreService.instance.gameTime));
        text.text = Mathf.FloorToInt(time / 60).ToString("00") + ":" + (time % 60).ToString("00");
    }
}
