using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementType {Horizontal, Vertical}
    public MovementType movementType = MovementType.Horizontal;

    public float speed = 2f;
    public float movementRange = 3f;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movementType == MovementType.Horizontal)
        {
            transform.position = new Vector2(
                startPosition.x + Mathf.PingPong(Time.time * speed, movementRange) - movementRange / 2,
                transform.position.y
            );
        }
        else if (movementType == MovementType.Vertical)
        {
            transform.position = new Vector2(
                transform.position.x,
                startPosition.y + Mathf.PingPong(Time.time * speed, movementRange) - movementRange / 2
            );
        }
    }
}
