using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float focusedSpeed = 2.5f;
    public float normalFireRate = 0.1f;
    public float focusedFireRate = 0.05f;
    private float currentSpeed;
    private float currentFireRate;
    private bool isShooting = false;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    private Rigidbody2D body;
    private PlayerAiming playerAiming;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAiming = GetComponent<PlayerAiming>();
        currentSpeed = normalSpeed;
        currentFireRate = normalFireRate;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = focusedSpeed;
            currentFireRate = focusedFireRate;
        }
        else
        {
            currentSpeed = normalSpeed;
            currentFireRate = normalFireRate;
        }
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        body.velocity = new Vector2(moveHorizontal * currentSpeed, moveVertical * currentSpeed);
    }

    private IEnumerator ShootContinuosly()
    {
        while (isShooting)
        {
            ShootProjectile();
            yield return new WaitForSeconds(currentFireRate);
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - projectileSpawnPoint.position).normalized;
        projectileRb.velocity = direction * 30f;
    }
}