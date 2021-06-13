using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInputBlocker : MonoBehaviour
{
    public Character character;

    public void StopInput()
    {
        character.inputEnabled = false;
    }

    public void EnableInput()
    {
        character.inputEnabled = true;
    }

    public void FinishPee()
    {
        PeePoint.activePoint.OnPeeFinished();
    }
}
