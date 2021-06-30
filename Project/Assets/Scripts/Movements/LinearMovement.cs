using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up, Down, Left, Right,
}

public enum LookDirection
{
    Right, Left
}

public enum LoopType
{
    Uturn,
    
}

[System.Serializable]
public struct LinearMovementConfig
{
    public float movementSpeed;
    public Direction direction;
    public float loopDuration;
    public LoopType uturn;
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
    private CharacterDisplay characterDisplay;
    private bool left = false;

    void Start()
    {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        characterDisplay = GetComponent<CharacterDisplay>();
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
                    characterDisplay.lookDirection = left ? LookDirection.Left : LookDirection.Right;
                }
            }
            else
            {
                transform.position = startPosition;
            }
        }
    }

    void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
    }
}
