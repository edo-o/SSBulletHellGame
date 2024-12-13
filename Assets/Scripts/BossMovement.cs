using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public float changePatternInterval = 5f;
    private float patternTimer;

    public enum MovementPattern { ZigZag, Circle, FollowPlayer, MoveToTarget, Idle }
    public MovementPattern currentPattern;

    //Zig-Zag settings
    public float zigZagSpeed = 5f;
    public float zigZagWidth = 3f;
    public float zigZagTimer = 0f;

    //Circle settings
    public float circularSpeed = 3f;
    public float circularRadius = 2f;
    public float circularAngle = 0f;

    //Follow Player settings
    private Transform player;
    public float followSpeed = 2f;

    //idle settings
    public float idleSpeed = 1f;
    private Vector3 idleTargetPosition;

    private Vector3 targetPosition;
    public float moveToTargetSpeed = 10f;

    public Vector2 movementBounds = new Vector2(10f, 5f);

    private Vector3 startPosition;
    private bool movingToTarget = true;
    private Vector3 currentTargetPosition;
    private Vector3 bossPos;


    private HealthSystem bossHealth;
    public enum BossPhase { Phase1, Phase2}
    public BossPhase currentPhase;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        currentTargetPosition = startPosition;
        startPosition = transform.position;
        SetRandomIdleTarget();


        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        bossHealth = GetComponent<HealthSystem>();

        currentPhase = BossPhase.Phase1;
    }

    
    void Update()
    {
        patternTimer += Time.deltaTime;
        if (patternTimer >= changePatternInterval)
        {
            RandomizeMovementPattern();
            patternTimer = 0f;
        }

        if (bossHealth.currentHealth <= bossHealth.maxHealth / 2)
        {
            currentPhase = BossPhase.Phase2;
        }
        if (movingToTarget)
        {
            MoveToTarget();
        }
        else if (currentPhase == BossPhase.Phase1)
        {
            Phase1();
            switch (currentPattern)
            {
                case MovementPattern.FollowPlayer:
                    FollowPlayerMovement();
                    break;
                case MovementPattern.Idle:
                    IdleMovement();
                    break;
            }
        }
        else if (currentPhase == BossPhase.Phase2)
        {
            Phase2();
            switch (currentPattern)
            {
                case MovementPattern.ZigZag:
                    ZigZagMovement();
                    break;
                case MovementPattern.Circle:
                    CircleMovement();
                    break;
                case MovementPattern.FollowPlayer:
                    FollowPlayerMovement();
                    break;
                case MovementPattern.Idle:
                    IdleMovement();
                    break;
            }
        }
    }

    private void RandomizeMovementPattern()
    {
        List<MovementPattern> validPatterns = new List<MovementPattern>();

        if (currentPhase == BossPhase.Phase1)
        {
            validPatterns.Add(MovementPattern.FollowPlayer);
            validPatterns.Add(MovementPattern.Idle);
        }
        else if (currentPhase == BossPhase.Phase2)
        {
            validPatterns.Add(MovementPattern.ZigZag);
            validPatterns.Add(MovementPattern.Circle);
            validPatterns.Add(MovementPattern.FollowPlayer);
            validPatterns.Add(MovementPattern.Idle);
        }

        int patternCount = validPatterns.Count;
        currentPattern = validPatterns[Random.Range(0, patternCount)];

        bossPos = transform.position;
    }

    public void SetTargetPosition(Vector2 targetPos)
    {
        targetPosition = targetPos;
        movingToTarget = true;
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveToTargetSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingToTarget = false;
            currentTargetPosition = targetPosition;
        }
    }

    private void IdleMovement ()
    {
        transform.position = Vector3.MoveTowards(transform.position, idleTargetPosition, idleSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, idleTargetPosition) < 0.1f)
        {
            SetRandomIdleTarget();
        }
    }

    private void ZigZagMovement()
    {
        zigZagTimer += Time.deltaTime * zigZagSpeed;
        float x = Mathf.Sin(zigZagTimer) * zigZagWidth;
        transform.position = new Vector3(bossPos.x + x, bossPos.y, 0);
    }

    private void CircleMovement()
    {
        circularAngle += Time.deltaTime * circularSpeed;
        float x = Mathf.Cos(circularAngle) * circularRadius;
        float y = Mathf.Sin(circularAngle) * circularRadius;
        transform.position = new Vector3(bossPos.x + x, bossPos.y + y, 0);
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

    private void SetRandomIdleTarget()
    {
        float x = Random.Range(-movementBounds.x, movementBounds.x);
        float y = Random.Range(-movementBounds.y, movementBounds.y);
        idleTargetPosition = new Vector3(x, y, 0);
    }
    private void Phase1()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
        followSpeed = 2f;
    }

    private void Phase2()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
        followSpeed = 3.5f;
        idleSpeed = 6f;
    }


}
