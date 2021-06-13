using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSensor : MonoBehaviour
{
    public System.Action triggeredDelegate;
    public UnityEngine.Events.UnityEvent triggeredEvent;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();
        if(character != null)
        {
            triggeredDelegate?.Invoke();
            triggeredEvent.Invoke();
        }
    }
}
