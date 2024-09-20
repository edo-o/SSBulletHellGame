using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        body.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

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
