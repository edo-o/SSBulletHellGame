using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 targetPosition;
    private bool hasTarget = false;


    void Update()
    {
        if (hasTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                hasTarget = false;
            }
        }
    }


    public void SetTargetPosition(Vector2 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}