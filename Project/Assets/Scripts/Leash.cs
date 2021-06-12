using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
    public Rigidbody2D parent;
    public Rigidbody2D target;
    private LineRenderer lineRenderer;
    public float leashLength = 5;
    public float segmentLength = 0.1f;

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
        lineRenderer.positionCount = leashElements.Count;
    }

    void Update()
    {
        Vector3[] positions = new Vector3[leashElements.Count + 2];
        for(int i=0; i<leashElements.Count; i++)
        {
            positions[i + 1] = leashElements[i].transform.position;
        }
        positions[0] = target.position;
        positions[leashElements.Count] = target.position;
        //positions[0] = parent.position;
        lineRenderer.SetPositions(positions);
    }
}
