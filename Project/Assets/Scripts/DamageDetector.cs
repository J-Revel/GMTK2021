using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    public System.Action<float> collisionDelegate;
    public UnityEngine.Events.UnityEvent collisionEvent;
    public float minVelocity = 3;
    public float invincibilityDuration = 1;
    private float invincibilityTime = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if(invincibilityTime > 0)
            invincibilityTime -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(invincibilityTime <= 0 && collision.relativeVelocity.sqrMagnitude > minVelocity * minVelocity)
        {
            invincibilityTime = invincibilityDuration;
            collisionDelegate?.Invoke(collision.relativeVelocity.magnitude);
            collisionEvent.Invoke();
        }
    }
}
