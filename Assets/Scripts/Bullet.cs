using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletLife = 1f;
    public float speed = 1f;

    private Vector2 spawnpoint;
    private float timer = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        if(timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

}
