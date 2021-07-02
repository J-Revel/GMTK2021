using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndZone : MonoBehaviour
{
    public Animator successElement;
    public Animator failureElement;
    public Transform instanceContainer;
    void Start()
    {
        GetComponent<CharacterSensor>().triggeredDelegate += OnEnteredZone;
    }

    // Update is called once per frame
    void OnEnteredZone(Character character)
    {
        bool success = true;
        foreach(var objective in GameLauncher.instance.config.objectives)
        {
            StatValue stat = ScoreService.instance.GetStat(objective.statName);
            if(objective.isDefeatCondition)
            {
                if(stat.value > stat.max)
                {
                    success = false;
                }
            }
            else
            {
                if(stat.value < stat.max)
                {
                    success = false;
                }
            }
        }
        if(success)
            successElement.SetTrigger("Play");
        else failureElement.SetTrigger("Play");
    }
}
