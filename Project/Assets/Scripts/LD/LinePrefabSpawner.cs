using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public enum LineDirection
{
    Vertical, Horizontal
}

[ExecuteInEditMode]
public class LinePrefabSpawner : MonoBehaviour
{
    public LineDirection lineDirection;
    public float targetDistance;
    public GameObject spawnParent;

    public Transform[] prefabs;
    public float spawnDistance;

    private bool isInPrefabEditor
    {
        get
        { 
#if UNITY_EDITOR
            return UnityEditor.SceneManagement.StageUtility.GetCurrentStage().GetType() == typeof(UnityEditor.Experimental.SceneManagement.PrefabStage);
#else
            return false;
#endif
        }
    }

    public Vector3 targetPoint 
    { 
        get 
        { 
            Vector3 direction = transform.right;
            if(lineDirection == LineDirection.Vertical)
                direction = transform.up;
            return transform.position + direction * targetDistance;
        }

        set
        {
            targetDistance = Vector3.Distance(transform.position, value);
        }
    }

    public Vector3 targetDirection
    {
        get
        {
            Vector3 direction = transform.right;
            if(lineDirection == LineDirection.Vertical)
                direction = transform.up;
            return direction;
        }
    }

    void Start()
    {
        UpdateElements();
    }

    public void UpdateElements()
    {
        for(int i=0; i<=transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        spawnParent = new GameObject("elements");
        spawnParent.transform.parent = transform;

        int instanceCount = (int)(targetDistance / spawnDistance);
        Vector3 direction = transform.right;
        if(lineDirection == LineDirection.Vertical)
            direction = transform.up;
        for(int i=0; i<=instanceCount; i++)
        {
            Vector3 position = transform.position + direction * (targetDistance * i / instanceCount);
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity, spawnParent.transform);
        }        
    }
}
