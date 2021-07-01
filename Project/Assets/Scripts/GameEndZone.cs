using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndZone : MonoBehaviour
{
    public Transform menuPrefab;
    void Start()
    {
        GetComponent<CharacterSensor>().triggeredDelegate += OnEnteredZone;
    }

    // Update is called once per frame
    void OnEnteredZone(Character character)
    {
        Instantiate(menuPrefab);
    }
}
