using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;

    public Vector2 direction;

    private Vector2 spawnpoint;
    private float timer = 0f;

    void Start()
    {
        spawnpoint = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if(timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
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

    


 private Vector2 Movement(float timer)
    {
        float x = timer * speed * transform.up.x;
        float y = timer * speed * transform.up.y;
        return new Vector2(x+spawnpoint.x, y+spawnpoint.y);
    }



}
