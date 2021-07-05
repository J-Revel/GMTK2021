using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNoVisibleChild : MonoBehaviour
{
    void Update()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.active)
                return;
        }
        gameObject.SetActive(false);
    }
}
