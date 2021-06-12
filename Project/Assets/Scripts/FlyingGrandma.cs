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
    public float takeoffForce = 900;
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.drag = groundedDrag;
        rigidbody.mass = groundedMass;
        initialRot = spriteRenderer.transform.localRotation;
        TargetJoint2D joint = rigidbody.gameObject.AddComponent<TargetJoint2D>();
        joint.breakForce = takeoffForce;
    }

    void Update()
    {
        currentVelocity = rigidbody.velocity.magnitude;
        bool isLeft = rigidbody.velocity.x < 0.01f;
        animator.SetBool("left", isLeft);
        
        if(!flying)
        {
            spriteRenderer.transform.rotation = initialRot;
            
        }
        else
        {
            float angle = 0;
            angle = Vector3.SignedAngle(Vector2.right, rigidbody.velocity, Vector3.forward);

            float angleRatio = Mathf.Abs(rigidbody.velocity.normalized.x);
            Debug.Log(angleRatio);
            spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle + (isLeft ? 180 : 0), Vector3.forward) * Quaternion.Slerp(Quaternion.identity, initialRot, angleRatio);
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
                    TargetJoint2D joint = rigidbody.gameObject.AddComponent<TargetJoint2D>();
                    joint.breakForce = takeoffForce;
                }
            }
            else
            {
                landingThresholdTime = 0;
            }
        }
    }

    void OnJointBreak2D(Joint2D joint)
    {
        
        flying = true;
        animator.SetBool("flying", true);
        rigidbody.drag = flyingDrag;
        rigidbody.mass = flyingMass;
        rigidbody.velocity = rigidbody.velocity.normalized * flyingLaunchSpeed;
    }
}
