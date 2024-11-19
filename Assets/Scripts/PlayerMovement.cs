using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed;
    public float dashForce;
    public float dashCooldownTime;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private bool canDash = true;
    private bool isShooting = false;
    private Rigidbody2D body;

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

if (Input.GetKey(KeyCode.Space))
{
    if (!isShooting)
    {
        isShooting = true;
        StartCoroutine(ShootContinuosly());
    }
}
else
{
    isShooting = false;
}
    } 

    private IEnumerator ShootContinuosly()
    {
        while (isShooting)
        {
            ShootProjectile();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = new Vector2(0, 30f);
    }
}