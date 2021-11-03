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
    public float pullDistance = 4;
    private new Rigidbody2D rigidbody;
    public float currentVelocity;
    public float flyingLaunchSpeed = 2;
    public float landingThresholdDuration = 0.5f;
    private float landingThresholdTime = 0;
    private Quaternion initialRot;
    public float takeoffForce = 900;
    public Transform dog;
    public float handHeight = 1;
    private bool fallen = false;
    
    public SpriteRenderer spriteRenderer;
    public AnimatedSprite animatedSprite;

    private float angleRatio;
    public float angleRatioSpeed = 1;
    private float flipAnimTime = 0;
    public float flipAnimDuration = 0.5f;
    public float testValue = 0;
    
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
        bool isLeft = rigidbody.transform.position.x > dog.transform.position.x;

        
        spriteRenderer.transform.localPosition = -Vector3.up * animatedSprite.GetLocalPointPosition("Ground").y;
        
        if(!flying)
        {
            spriteRenderer.transform.localRotation = initialRot;
            animatedSprite.spriteRenderer.flipX = isLeft;
            animatedSprite.spriteRenderer.flipY = false;
            
            if(fallen)
            {
                animatedSprite.SelectAnim("Fall");
                if(animatedSprite.isAnimationFinished)
                    fallen = false;
            }
            else if(Vector3.Distance(transform.position, dog.position) > pullDistance)
                animatedSprite.SelectAnim("Pull");
            else
                animatedSprite.SelectAnim("Idle");
        }
        else
        {
            angleRatio += Mathf.Clamp(rigidbody.velocity.normalized.x - angleRatio , -angleRatioSpeed * Time.deltaTime, angleRatioSpeed * Time.deltaTime);
            // angleRatio = Mathf.Cos(Time.time * 3.14f / 2);
            animatedSprite.SelectAnim("Fly");
            animatedSprite.spriteRenderer.flipX = false;
            float displayAngleRatio = angleRatio;
            if(angleRatio * flipAnimTime < 0)
            {
                displayAngleRatio = Mathf.Sign(flipAnimTime) * 0.001f;
            }
            flipAnimTime += Mathf.Sign(angleRatio) * Time.deltaTime;
            flipAnimTime = Mathf.Clamp(flipAnimTime, -flipAnimDuration, flipAnimDuration);
            animatedSprite.spriteRenderer.flipY = displayAngleRatio < 0;
            float angle = 0;
            angle = Vector3.SignedAngle(Vector2.right, rigidbody.velocity, Vector3.forward);
            
            print(flipAnimTime + " " + displayAngleRatio);
            //- 90 * Mathf.Sign(flipAnimTime) * ((1-Mathf.Abs(flipAnimTime / flipAnimDuration)))
            spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.AngleAxis(-displayAngleRatio * 45 - 90 * Mathf.Sign(flipAnimTime) * (1-Mathf.Abs(flipAnimTime) / flipAnimDuration), Vector3.right);
            
            if(rigidbody.velocity.sqrMagnitude < velocityLandingThreshold * velocityLandingThreshold)
            {
                landingThresholdTime += Time.deltaTime;
                if(landingThresholdTime > landingThresholdDuration)
                {
                    rigidbody.velocity = Vector3.zero;
                    flying = false;
                    fallen = true;
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
        rigidbody.drag = flyingDrag;
        rigidbody.mass = flyingMass;
        rigidbody.velocity = -joint.reactionForce.normalized * flyingLaunchSpeed;
        angleRatio = rigidbody.velocity.normalized.x;
    }
}
