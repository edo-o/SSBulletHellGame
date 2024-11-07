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

AimTowardsMouse();

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

    private void AimTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0f;  // Ensure it's at 0 in the z-axis for 2D

    // Calculate the direction from player to mouse
    Vector2 shootDirection = (mousePosition - projectileSpawnPoint.position).normalized;

    // Instantiate the bullet and set its direction
    GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
    Bullet bulletScript = projectile.GetComponent<Bullet>();
    bulletScript.direction = shootDirection;
    }
}

