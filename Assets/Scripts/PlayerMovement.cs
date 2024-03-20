using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D body;
    private bool isGrounded;
    private bool hasJumped = false;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
     isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    if (isGrounded)
    {
        hasJumped = false;
    }

    if (body.velocity.y < 0)
    {
        body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
    else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
    {
        body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

    if (Input.GetButtonDown("Jump") && isGrounded && !hasJumped)
    {
        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        hasJumped = true;
    }
    }   
}
