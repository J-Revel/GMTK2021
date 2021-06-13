using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float runSpeed = 1;
    public float acceleration;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.AddForce(inputVector.normalized * acceleration, ForceMode2D.Force);
        if(Input.GetAxis("Horizontal") > 0)
            animator.SetBool("left", false);
        if(Input.GetAxis("Horizontal") < 0)
            animator.SetBool("left", true);
        animator.SetBool("run", inputVector.sqrMagnitude > 0.5);
        animator.SetFloat("speed", rigidbody.velocity.sqrMagnitude);
    }
}
