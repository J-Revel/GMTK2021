using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGrandma : MonoBehaviour
{
    public float velocityFlyingThreshold = 1;
    public float velocityLandingThreshold = 0.5f;
    public float flyingDrag = 5;
    public float flyingMass = 2;
    public float groundedDrag = 20;
    public float groundedMass = 4;
    private bool flying = false;
    private new Rigidbody2D rigidbody;
    public float currentVelocity;
    public float flyingLaunchSpeed = 2;
    public float landingThresholdDuration = 0.5f;
    private float landingThresholdTime = 0;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.drag = groundedDrag;
        rigidbody.mass = groundedMass;
    }

    void Update()
    {
        currentVelocity = rigidbody.velocity.magnitude;
        if(!flying)
        {
            if(rigidbody.velocity.sqrMagnitude > velocityFlyingThreshold * velocityFlyingThreshold)
            {
                flying = true;
                rigidbody.drag = flyingDrag;
                rigidbody.mass = flyingMass;
                rigidbody.velocity = rigidbody.velocity.normalized * flyingLaunchSpeed;
            }
        }
        else
        {
            if(rigidbody.velocity.sqrMagnitude < velocityLandingThreshold * velocityLandingThreshold)
            {
                landingThresholdTime += Time.deltaTime;
                if(landingThresholdTime > landingThresholdDuration)
                {
                    rigidbody.velocity = Vector3.zero;
                    flying = false;
                    rigidbody.drag = groundedDrag;
                    rigidbody.mass = groundedMass;
                }
            }
            else
            {
                landingThresholdTime = 0;
            }
        }
    }
}
