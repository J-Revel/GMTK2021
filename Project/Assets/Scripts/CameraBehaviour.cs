using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Rigidbody2D mainTarget;
    public Rigidbody2D secondaryTarget;
    
    public float drag = 0.98f;
    public float acceleration = 5;
    public float speedAdvance = 5;
    private Vector3 velocity = Vector3.zero;
    public float secondaryTargetWeight = 0.3f;

    public Vector3[] velocities;
    public int velocitySampleCount = 100;
    private int velocitySampleIndex = 0;


    void Start()
    {
        velocities = new Vector3[velocitySampleCount];
    }

    void Update()
    {
        velocities[velocitySampleIndex] = mainTarget.velocity;
        Vector3 meanVelocity = Vector2.zero;
        for(int i=0; i<velocitySampleCount; i++)
        {
            meanVelocity += velocities[i];
        } 
        meanVelocity /= velocitySampleCount;
        Vector3 targetPosition = (mainTarget.transform.position + meanVelocity * speedAdvance) * (1 - secondaryTargetWeight);
        targetPosition += secondaryTargetWeight * secondaryTarget.transform.position;
        targetPosition.z = transform.position.z;
        Vector3 targetDirection = targetPosition - transform.position;
        velocity *= Mathf.Pow(drag, Time.deltaTime);
        velocity += targetDirection.normalized * Time.deltaTime * acceleration;
        transform.position = targetPosition;
    }
}
