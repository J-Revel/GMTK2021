using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableAnimation : MonoBehaviour
{
    private float collisionDirection;
    public float animDuration = 0.3f;
    private float animTime = 0;
    private bool animStarted = false;
    public Transform displayTransform;
    private Quaternion startRotation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Character>() != null)
        {
            collisionDirection = Vector2.SignedAngle(Vector2.up, collision.collider.GetComponent<Rigidbody2D>().velocity);
            
            Debug.Log(collisionDirection);
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
            Quaternion targetRotation = Quaternion.AngleAxis(collisionDirection, Vector3.forward);
            // Debug.Log(collisionDirection);
            if(collisionDirection >= 90 || collisionDirection < -90)
                targetRotation = Quaternion.AngleAxis(180 - collisionDirection, Vector3.forward) * Quaternion.Euler(180, 0, 0);
            displayTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, animTime / animDuration);
            //else
            // {
                
            // }
            // transform.rotation = Quaternion.Lerp();
        }
    }
}
