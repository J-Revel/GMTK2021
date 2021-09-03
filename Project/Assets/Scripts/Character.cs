using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterState
{
    Idle,
    Moving,
    Stop,

}

public class Character : MonoBehaviour
{
    
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private AnimatedSprite animatedSprite;

    // public Animator animator;
    public float runSpeed = 1;
    public float acceleration;
    public float footstepSoundFadeTime = 0;
    public float footstepSoundFadeDuration = 0.5f;
    public Vector2 forcedInput;
    public AudioSource footstepSource;
    public bool inputEnabled = true;
    private Vector2 input;
    
    private float peeDuration = 2;
    private float peeTime = 0;
    private bool peeing = false;
    private CharacterState characterState = CharacterState.Idle;
    public Transform testPosition;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animatedSprite = GetComponentInChildren<AnimatedSprite>();
    }

    void FixedUpdate()
    {
        testPosition.position = animatedSprite.GetPointPosition("Leash");
        Vector2 usedInput = input;
        if(!inputEnabled)
            usedInput = Vector3.zero;
        rigidbody.AddForce(usedInput.normalized * acceleration * rigidbody.mass, ForceMode2D.Force);
        if(rigidbody.velocity.sqrMagnitude > 1)
        {
            animatedSprite.SelectAnim("run");
            characterState = CharacterState.Moving;
        }
        switch(characterState)
        {
            case CharacterState.Idle:
                if(peeing)
                {
                    animatedSprite.SelectAnim("pee");
                }
                break;
            case CharacterState.Moving:
                if(usedInput.sqrMagnitude < 0.25f)
                {
                    animatedSprite.SelectAnim("stop");
                    characterState = CharacterState.Stop;
                }
                break;
            case CharacterState.Stop:
                if(rigidbody.velocity.sqrMagnitude < 1)
                {
                    animatedSprite.SelectAnim("idle");
                    characterState = CharacterState.Idle;
                }
                break;
        }
        if(usedInput.x > 0)
            animatedSprite.spriteRenderer.flipX = true;
        if(usedInput.x < 0)
            animatedSprite.spriteRenderer.flipX = false;
        
        // animator.SetBool("run", input.sqrMagnitude > 0.01);
        // animator.SetFloat("speed", rigidbody.velocity.sqrMagnitude);
        if(footstepSource != null)
        {
            if(usedInput.sqrMagnitude > 0.5)
            {
                footstepSource.volume = Mathf.Min((usedInput.magnitude - 0.7f) * 3, 1);
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
                animatedSprite.SelectAnim("idle");
                inputEnabled = true;
                PeePoint.activePoint.OnPeeFinished();
            }
        }
    }

    public void StartPeing()
    {
        // animator.SetBool("canPee", true);
        peeTime = 0;
        peeing = true;
        inputEnabled = false;
    }

    private void OnMovement(InputValue value)
    {
        this.input = value.Get<Vector2>();
    }
}
