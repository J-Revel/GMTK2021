using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up, Down, Left, Right,
}
public class LinearMovement : MonoBehaviour
{
    public Direction direction;
    public float speed = 1;
    public float loopDuration = 10;
    private float time = 0;
    private Vector3 startPosition;
    private new Rigidbody2D rigidbody;
    public bool uturn = false;
    private Animator animator;
    private bool left = false;

    void Start()
    {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        switch(direction)
        {
            case Direction.Up:
                rigidbody.velocity = new Vector3(0, speed);
                break;
            case Direction.Down:
                rigidbody.velocity = new Vector3(0, -speed);
                break;
            case Direction.Left:
                rigidbody.velocity = new Vector3(-speed, 0);
                left = true;
                break;
            case Direction.Right:
                rigidbody.velocity = new Vector3(speed, 0);
                break;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > loopDuration)
        {
            time = 0;
            if(uturn)
            {
                rigidbody.velocity = - rigidbody.velocity;
                if(direction == Direction.Right || direction == Direction.Left)
                {
                    left = !left;
                }
            }
            else
            {
                transform.position = startPosition;
            }
        }
        if(animator != null)
            animator.SetBool("left", left);
    }
}
