using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public float acceleration;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.AddForce((new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))).normalized * acceleration, ForceMode2D.Force);
        if(Input.GetAxis("Horizontal") > 0)
            spriteRenderer.flipX = true;
        if(Input.GetAxis("Horizontal") < 0)
            spriteRenderer.flipX = false;
    }
}
