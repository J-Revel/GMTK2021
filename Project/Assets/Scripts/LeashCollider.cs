using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeashCollider : MonoBehaviour
{
    private new CapsuleCollider2D collider;
    public DistanceJoint2D joint;
    public Vector3 rotAxis = Vector3.right;
    void Start()
    {
        collider = gameObject.AddComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        Vector2 parentDirection = joint.connectedBody.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(parentDirection, Vector3.forward) * Quaternion.AngleAxis(90, rotAxis);
        collider.size = new Vector2(0.02f, parentDirection.magnitude);
    }
}
