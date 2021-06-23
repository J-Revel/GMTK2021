using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[ExecuteInEditMode]
public class LinePrefabSpawner : MonoBehaviour
{
    public Transform start;
    public Transform target;
    public Transform spawnParent;

    public Transform[] prefabs;
    public float spawnDistance;

    void Start()
    {
        
    }

    void Update()
    {
        bool isInPrefabEditor = UnityEditor.SceneManagement.StageUtility.GetCurrentStage().GetType() == typeof(UnityEditor.Experimental.SceneManagement.PrefabStage));
        if(!Application.isPlaying && prefabs.Length > 0 && target != null && !isInPrefabEditor)
        {
            for(int i=0; i<spawnParent.childCount; i++)
            {
                DestroyImmediate(spawnParent.GetChild(spawnParent.childCount - 1 - i).gameObject);
            }
            float distance = Vector3.Distance(start.position, target.position);
            int instanceCount = (int)(distance / spawnDistance);
            for(int i=0; i<=instanceCount; i++)
            {
                Vector3 position = Vector3.Lerp(start.position, target.position, (float)i / instanceCount);
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity, spawnParent);
            }
        }
    }
}
