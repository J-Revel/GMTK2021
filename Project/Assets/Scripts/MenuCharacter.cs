using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacter : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float runSpeed = 1;
    public float acceleration;
    public float footstepSoundFadeTime = 0;
    public float footstepSoundFadeDuration = 0.5f;
    public AudioSource footstepSource;
    public Vector2 mainInputVector = Vector2.right;
    public float runTimeMin = 4;
    public float runTimeMax = 8;
    public float stopTimeMin = 2;
    public float stopTimeMax = 7;
    private float time = 5;
    private bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            running = !running;
            if(running)
                time += Random.Range(runTimeMin, runTimeMax);
            else
                time += Random.Range(stopTimeMin, stopTimeMax);
        }
        Vector2 inputVector = mainInputVector * (running ? 1 : 0);
        rigidbody.AddForce(inputVector.normalized * acceleration, ForceMode2D.Force);
        if(inputVector.x > 0)
            animator.SetBool("left", false);
        if(inputVector.x < 0)
            animator.SetBool("left", true);
        animator.SetBool("run", inputVector.sqrMagnitude > 0.5);
        animator.SetFloat("speed", rigidbody.velocity.sqrMagnitude);
        if(inputVector.sqrMagnitude > 0.5)
        {
            footstepSource.volume = Mathf.Min((inputVector.magnitude - 0.7f) * 3, 1);
        }
        else footstepSource.volume = 0;
    }
}
