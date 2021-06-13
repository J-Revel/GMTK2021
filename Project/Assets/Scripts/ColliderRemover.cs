using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRemover : MonoBehaviour
{
    public void RemoveCollider()
    {
        Destroy(GetComponent<Collider>());
    }
}
