using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public Transform prefab;
    public Transform instanceContainer;

    public void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation, instanceContainer);
    }
}
