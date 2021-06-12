using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
    public Rigidbody2D parent;
    public Rigidbody2D target;
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
            if(previousJoint == null)
            {
                Debug.Log(previousJoint);
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
    }

    void Update()
    {
        
    }
}
