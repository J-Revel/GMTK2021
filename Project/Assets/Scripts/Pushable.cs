using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    public GameObject toHide;
    public GameObject toShow;
    public Transform[] fx;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Character>() != null)
        {
            toHide.SetActive(false);
            toShow.SetActive(true);
        }
        for(int i=0; i<fx.Length; i++)
        {
            Instantiate(fx[i], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
