using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float destroyDelay = 2;
    private float time = 0;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
    }
}
