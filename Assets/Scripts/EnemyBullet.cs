using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1;
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 5f;
    public GameObject enemyBulletPrefab;

    public Vector2 direction;

    private Vector2 spawnpoint;
    private float timer = 0f;

    void Start()
    {
        spawnpoint = new Vector2(transform.position.x, transform.position.y);
        timer = 0f;
    }

    void Update()
    {
        if(timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;

        transform.position = (Vector3)direction * speed * Time.deltaTime;
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

    public void ShootAtPlayer(Transform playerTransform)
    {
        Vector2 shootDirection = (playerTransform.position - transform.position).normalized;

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        EnemyBullet enemyBulletScript = bullet.GetComponent<EnemyBullet>();
        enemyBulletScript.direction = shootDirection;
    }

    

/*
 private Vector2 Movement(float timer)
    {
        float x = timer * speed * transform.up.x;
        float y = timer * speed * transform.up.y;
        return new Vector2(x+spawnpoint.x, y+spawnpoint.y);
    }


*/
}