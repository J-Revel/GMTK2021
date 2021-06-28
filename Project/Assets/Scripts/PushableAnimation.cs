using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableAnimation : MonoBehaviour
{
    private Vector2 collisionDirection;
    public float animDuration = 0.3f;
    private float animTime = 0;
    private bool animStarted = false;
    public Transform displayTransform;
    private Quaternion startRotation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Character>() != null)
        {
            collisionDirection = transform.position - collision.collider.transform.position;
            
            animStarted = true;
            Destroy(collision.otherCollider);
        }
    }

    void Start()
    {
        startRotation = displayTransform.rotation;
    }

    void Update()
    {
        if(animStarted)
        {
            animTime += Time.deltaTime;
            float upAngle = Vector2.SignedAngle(Vector2.up, collisionDirection);
            Quaternion targetRotation = Quaternion.AngleAxis(upAngle, Vector3.forward);
            if(collisionDirection.y > 0)
            {
                

            }
            else
            {
                float downAngle = Vector2.SignedAngle(Vector2.down, collisionDirection);
                targetRotation = Quaternion.AngleAxis(180, Vector3.right) * Quaternion.AngleAxis(downAngle, -Vector3.forward);
            }
            displayTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, animTime / animDuration);
            //else
            // {
                
            // }
            // transform.rotation = Quaternion.Lerp();
        }
    }
}
