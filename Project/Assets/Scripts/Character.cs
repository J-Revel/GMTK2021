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
    public float footstepSoundFadeTime = 0;
    public float footstepSoundFadeDuration = 0.5f;
    public Vector2 forcedInput;
    public AudioSource footstepSource;
    public bool inputEnabled = true;
    
    private float peeDuration = 2;
    private float peeTime = 0;
    private bool peeing = false;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(!inputEnabled)
            inputVector = Vector3.zero;
        rigidbody.AddForce(inputVector.normalized * acceleration * rigidbody.mass, ForceMode2D.Force);
        if(Input.GetAxis("Horizontal") > 0)
            animator.SetBool("left", false);
        if(Input.GetAxis("Horizontal") < 0)
            animator.SetBool("left", true);
        animator.SetBool("run", inputVector.sqrMagnitude > 0.01);
        animator.SetFloat("speed", rigidbody.velocity.sqrMagnitude);
        if(footstepSource != null)
        {
            if(inputVector.sqrMagnitude > 0.5)
            {
                footstepSource.volume = Mathf.Min((inputVector.magnitude - 0.7f) * 3, 1);
            }
            else footstepSource.volume = 0;
        }
        if(peeing)
        {
            peeTime += Time.fixedDeltaTime;
            if(peeTime > peeDuration)
            {
                peeTime = 0;
                peeing = false;
                animator.SetBool("canPee", false);
                inputEnabled = true;
                PeePoint.activePoint.OnPeeFinished();
            }
        }
    }

    public void StartPeing()
    {
        animator.SetBool("canPee", true);
        peeTime = 0;
        peeing = true;
        inputEnabled = false;
    }
}
