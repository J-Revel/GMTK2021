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
    private Quaternion initialRot;
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.drag = groundedDrag;
        rigidbody.mass = groundedMass;
        initialRot = spriteRenderer.transform.localRotation;
    }

    void Update()
    {
        currentVelocity = rigidbody.velocity.magnitude;
        animator.SetBool("left", rigidbody.velocity.x < 0.01f);
        
        if(!flying)
        {
            spriteRenderer.transform.rotation = initialRot;
            if(rigidbody.velocity.sqrMagnitude > velocityFlyingThreshold * velocityFlyingThreshold)
            {
                flying = true;
                animator.SetBool("flying", true);
                rigidbody.drag = flyingDrag;
                rigidbody.mass = flyingMass;
                rigidbody.velocity = rigidbody.velocity.normalized * flyingLaunchSpeed;
            }
        }
        else
        {
            float angle = 0;
            if(rigidbody.velocity.x > 0)
            {
                angle = Vector3.SignedAngle(Vector2.right, rigidbody.velocity, Vector3.forward);
            }
            else
            {
                angle = Vector3.SignedAngle(Vector2.left, rigidbody.velocity, Vector3.forward);
            }
            spriteRenderer.transform.rotation = initialRot * Quaternion.AngleAxis(angle, Vector3.forward);
            if(rigidbody.velocity.sqrMagnitude < velocityLandingThreshold * velocityLandingThreshold)
            {
                landingThresholdTime += Time.deltaTime;
                if(landingThresholdTime > landingThresholdDuration)
                {
                    animator.SetBool("flying", false);
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
