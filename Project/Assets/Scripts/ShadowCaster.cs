using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCaster : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.receiveShadows = true;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
