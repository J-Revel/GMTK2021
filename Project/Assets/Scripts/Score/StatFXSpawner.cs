using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatFXSpawner : MonoBehaviour
{
    public string stat;
    public StatDisplay prefab;
    public Transform instanceContainer;

    public void Spawn()
    {
        if(GameLauncher.instance == null)
            return;
        if(GameLauncher.instance.config.isStatActive(stat))
        {
            Instantiate(prefab, transform.position, Quaternion.identity, instanceContainer).stat = stat;
        }
    }
}
