using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    public enum MovementPattern {Static, ZigZag, Circle, FollowPlayer, MoveToTarget }
    public MovementPattern currentPattern = MovementPattern.Static;

    //Zig-Zag settings
    public float zigZagSpeed = 5f;
    public float zigZagWidth = 3f;
    public float zigZagTimer = 0f;

    //Circle settings
    public float circularSpeed = 3f;
    public float circularRadius = 2f;
    public float circularAngle = 0f;

    //Follow Player settings
    public Transform player;
    public float followSpeed = 2f;

    private Vector2 targetPosition;
    public float moveToTargetSpeed = 2f;

    public Vector2 movementBounds = new Vector2(10f, 5f);

    private Vector3 startPosition;


    void Start()
    {
        startPosition = transform.position;
    }

    
    void Update()
    {
        switch (currentPattern)
        {
            case MovementPattern.Static:
                StaticMovement();
                break;
            case MovementPattern.ZigZag:
                ZigZagMovement();
                break;
            case MovementPattern.Circle:
                CircleMovement();
                break;
            case MovementPattern.FollowPlayer:
                FollowPlayerMovement();
                break;
        }
    }

    private void StaticMovement()
    {
        //Do nothing
    }

    private void ZigZagMovement()
    {
        zigZagTimer += Time.deltaTime * zigZagSpeed;
        float x = Mathf.Sin(zigZagTimer) * zigZagWidth;
        transform.position = new Vector3(startPosition.x + x, startPosition.y, 0);
    }

    private void CircleMovement()
    {
        circularAngle += Time.deltaTime * circularSpeed;
        float x = Mathf.Cos(circularAngle) * circularRadius;
        float y = Mathf.Sin(circularAngle) * circularRadius;
        transform.position = new Vector3(startPosition.x + x, startPosition.y + y, 0);
    }

    private void FollowPlayerMovement()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -movementBounds.x, movementBounds.x),
                Mathf.Clamp(transform.position.y, -movementBounds.y, movementBounds.y),
                transform.position.z
            );
        }
    }

    private void MoveToTarget()
    {
        Vector3 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction * moveToTargetSpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Switch to the desired movement pattern once the boss reaches the target position
            currentPattern = MovementPattern.ZigZag; // Change this to the desired pattern
        }
    }

    public void SetTargetPosition(Vector2 target)
    {
        targetPosition = target;
        currentPattern = MovementPattern.MoveToTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(movementBounds.x * 2, movementBounds.y * 2, 0));
    }
}
