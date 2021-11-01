using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
    public Rigidbody2D parent;
    public Rigidbody2D target;

    public Transform parentPosition;
    public Transform targetPosition;
    private LineRenderer lineRenderer;
    public float leashLength = 5;
    public float segmentLength = 0.1f;
    public float leashForce;
    public float zOffset = 1;
    public float pow = 1;
    public float upVectorAngle = 45;
    private Vector3 upVector;
    public float displayOffsetRatio = 1;

    public List<DistanceJoint2D> leashElements = new List<DistanceJoint2D>();

    public DistanceJoint2D leashElementPrefab;

    public int skippedElementCount = 3;

    void Start()
    {
        int segmentCount = Mathf.RoundToInt(leashLength / segmentLength);
        
        DistanceJoint2D previousJoint = null;
        for(int i=0; i<segmentCount; i++)
        {
            DistanceJoint2D currentJoint = Instantiate(leashElementPrefab, Vector3.Lerp(target.position, parent.position, (float)i / segmentCount), Quaternion.identity);
            

            leashElements.Add(currentJoint);
            if(previousJoint == null)
            {
                currentJoint.connectedBody = target; 
            }
            else
            {
                currentJoint.connectedBody = previousJoint.GetComponent<Rigidbody2D>();
            }
            currentJoint.distance = segmentLength;
            previousJoint = currentJoint;
        }
        DistanceJoint2D parentJoint = parent.gameObject.AddComponent<DistanceJoint2D>();
        parentJoint.connectedBody = previousJoint.GetComponent<Rigidbody2D>();
        parentJoint.distance = segmentLength;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = leashElements.Count + 2 - skippedElementCount;

        for(int i=0; i<leashElements.Count; i++)
        {
            for(int j=i + 2; j<leashElements.Count; j++)
            {
                DistanceJoint2D newJoint = leashElements[i].gameObject.AddComponent<DistanceJoint2D>();
                newJoint.connectedBody = leashElements[j].GetComponent<Rigidbody2D>();
                newJoint.autoConfigureDistance = false;
                newJoint.distance = (j - i) * segmentLength;
                newJoint.maxDistanceOnly = true;
            }
        }
    }

    void Update()
    {
        Vector3[] positions = new Vector3[leashElements.Count + 2 - skippedElementCount];
        positions[0] = targetPosition.position;
        upVector = Quaternion.Euler(upVectorAngle, 0, 0) * Vector3.up;
        float targetHeight = targetPosition.position.z/ upVector.z;
        float parentHeight = parentPosition.position.z/ upVector.z;
        positions[leashElements.Count + 1 - skippedElementCount] = parentPosition.position;
        for(int i=skippedElementCount; i<leashElements.Count; i++)
        {
            Vector3 newPosition = leashElements[i].transform.position;

            float ratio = (float)i / leashElements.Count;
            
            positions[i + 1 - skippedElementCount] = newPosition + upVector * (targetHeight * Mathf.Pow((1 - ratio), pow) + parentHeight * Mathf.Pow(ratio, pow));
        }
        lineRenderer.SetPositions(positions);
    }

    public void FixedUpdate()
    {
        leashForce = leashElements[0].reactionForce.sqrMagnitude;
    }
}
