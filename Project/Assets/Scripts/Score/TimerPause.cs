using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerPause : MonoBehaviour
{
    void Start()
    {
        ScoreService.instance.timerPaused = true;
    }
}
