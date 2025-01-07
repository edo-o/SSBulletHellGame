using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
                StartCoroutine(ShootContinuously());
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

    private IEnumerator ShootContinuously()
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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)projectileSpawnPoint.position).normalized;
        float projectileSpeed = 30f; // Fixed speed for the projectile
        projectileRb.velocity = direction * projectileSpeed;
    }
}