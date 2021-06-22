using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeePoint : MonoBehaviour
{
    public static PeePoint activePoint;
    public Transform fxPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if(character != null)
        {
            character.SetCanPee(true);
            activePoint = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if(character != null)
        {
            character.SetCanPee(false);
        }
    }

    public void OnPeeFinished()
    {
        Instantiate(fxPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
