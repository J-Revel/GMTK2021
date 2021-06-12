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
            }
        }
        else
        {
            if(rigidbody.velocity.sqrMagnitude < velocityLandingThreshold * velocityLandingThreshold)
            {
                flying = false;
                rigidbody.drag = groundedDrag;
                rigidbody.mass = groundedMass;
            }
        }
    }
}
