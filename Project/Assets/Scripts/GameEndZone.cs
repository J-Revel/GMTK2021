using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndZone : MonoBehaviour
{
    public Animator successElement;
    public Animator failureElement;
    public Transform instanceContainer;
    public float transitionDelay = 3;
    private float transitionTime;
    private bool doingTransition = false;
    private SceneTransition sceneTransition;
    void Start()
    {
        GetComponent<CharacterSensor>().triggeredDelegate += OnEnteredZone;
        sceneTransition = GetComponent<SceneTransition>();
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
        {
            successElement.SetTrigger("Play");
            doingTransition = true;
        }
        else failureElement.SetTrigger("Play");
    }

    void Update()
    {
        if(doingTransition)
        {
            transitionTime += Time.deltaTime;
            if(transitionTime >= transitionDelay)
            {
                sceneTransition.StartTransition();
                doingTransition = false;
            }
        }
    }
}
