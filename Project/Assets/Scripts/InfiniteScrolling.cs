using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrolling : MonoBehaviour
{
    public Transform character;
    public Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = character.position - transform.position;
        if(Vector3.Dot(delta, offset.normalized) / delta.magnitude > 0.5)
        {
            transform.position += offset;
        }
    }
}
