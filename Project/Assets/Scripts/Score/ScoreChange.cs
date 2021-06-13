using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChange : MonoBehaviour
{
    public int bonus = 100;

    void Start()
    {
        ScoreService.instance.currentScore += bonus;
    }
}
