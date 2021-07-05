using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndHandler : MonoBehaviour
{
    
    public Animator successElement;
    public Animator failureElement;
    public Animator unfinishedElement;

    private bool finished = false;

    void Start()
    {
        
    }

    public void OnSuccessFinish()
    {
        if(!finished)
        {
            finished = true;
            successElement.SetTrigger("Play");
        }
    }

    public void OnFailureFinish()
    {
        if(!finished)
        {
            finished = true;
            failureElement.SetTrigger("Appear");
        }
    }

    public void OnUnfinished()
    {
        if(!finished)
        {
            unfinishedElement.SetTrigger("Play");
        }
    }
}
