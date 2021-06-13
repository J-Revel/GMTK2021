using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSensorFX : MonoBehaviour
{
    public Transform[] prefabs;
    void Start()
    {
        GetComponent<CharacterSensor>().triggeredDelegate += OnDamage;
    }

    private void OnDamage()
    {
        for(int i=0; i<prefabs.Length; i++)
        {
            Instantiate<Transform>(prefabs[i], transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
