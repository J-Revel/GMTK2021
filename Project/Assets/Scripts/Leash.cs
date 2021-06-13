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

    public List<DistanceJoint2D> leashElements = new List<DistanceJoint2D>();

    public DistanceJoint2D leashElementPrefab;

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
        lineRenderer.positionCount = leashElements.Count + 2;
    }

    void Update()
    {
        Vector3[] positions = new Vector3[leashElements.Count + 2];
        positions[0] = targetPosition.position;
        positions[leashElements.Count + 1] = parentPosition.position;
        for(int i=0; i<leashElements.Count; i++)
        {
            Vector3 newPosition = leashElements[i].transform.position;
            //newPosition.z -= zOffset;

            Vector3 targetOffset = targetPosition.position - leashElements[1].transform.position;
            Vector3 parentOffset = parentPosition.position - leashElements[leashElements.Count - 2].transform.position;
            
            float ratio = (float)i / leashElements.Count;

            // Vector3 targetPosition = Vector3.Lerp(positions[0], positions[leashElements.Count + 1], ratio);
            // targetPosition.x = newPosition.x;
            // targetPosition.y = newPosition.y;
            
            positions[i + 1] = newPosition + targetOffset * (1 - Mathf.Pow(ratio, pow)) + parentOffset * Mathf.Pow(ratio, pow);
        }
        //positions[0] = parent.position;
        lineRenderer.SetPositions(positions);
    }

    public void FixedUpdate()
    {
        leashForce = leashElements[0].reactionForce.sqrMagnitude;
    }
}
