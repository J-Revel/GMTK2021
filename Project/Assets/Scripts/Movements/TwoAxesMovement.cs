using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoAxesMovement : MonoBehaviour
{
    public Vector2 input;
    
    private new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public float runSpeed = 1;
    public float acceleration = 100;
    public float footstepSoundFadeTime = 0;
    public float footstepSoundFadeDuration = 0.5f;
    public AudioSource footstepSource;
    public bool inputEnabled = true;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
