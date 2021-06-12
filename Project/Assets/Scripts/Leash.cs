using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
    public Rigidbody parent;
    public Rigidbody target;
    public float leashLength = 5;
    public float segmentLength = 0.1f;

    public List<DistanceJoint2D> leashElements = new List<DistanceJoint2D>();

    public DistanceJoint2D leashElementPrefab;

    void Start()
    {
        int segmentCount = Mathf.RoundToInt(leashLength / segmentLength);
        
        DistanceJoint2D previousElement = null;
        for(int i=0; i<segmentCount; i++)
        {
            DistanceJoint2D leashElement = Instantiate(leashElementPrefab, Vector3.Lerp(parent.position, target.position, (float)i / segmentCount), Quaternion.identity);
            if(previousElement != null)
            {
                //leashElement.attachedRigidbody = 
            }
        }
    }

    void Update()
    {
        
    }
}
