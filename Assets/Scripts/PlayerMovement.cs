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
    public LayerMask groundLayers;
    private bool isGrounded;
    private bool hasJumped = false;
    public float dashForce;
    public float dashCooldownTime;
    private bool canDash = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

     private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
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

    if(Input.GetButtonDown("Dash"))
{
    Debug.Log("Dash button pressed");
    if(canDash)
    
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 dashDirection;

        if (horizontalInput == 0 && verticalInput == 0)
        {
            dashDirection = new Vector2(1, 0);
        }

        else if (verticalInput == 0)
        {
            dashDirection = new Vector2(horizontalInput, 0);
        }
        else
        {
            dashDirection = new Vector2(horizontalInput, verticalInput);
        }

        body.AddForce(dashDirection.normalized * dashForce, ForceMode2D.Impulse);
        canDash = false;
        StartCoroutine(DashCooldown());
    }
}
    }   
}
