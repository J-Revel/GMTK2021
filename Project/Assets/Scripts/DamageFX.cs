using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFX : MonoBehaviour
{
    public Transform[] prefabs;
    void Start()
    {
        GetComponent<DamageDetector>().collisionDelegate += OnDamage;
    }

    private void OnDamage(float damage)
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
