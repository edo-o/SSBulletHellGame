using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife;
    public float speed; 
    private GameObject player;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
       timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin)
        {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if(bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            if (spawnerType == SpawnerType.Straight && player != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                spawnedBullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
            else
            {
                spawnedBullet.transform.rotation = transform.rotation;
                spawnedBullet.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            }
        }
    }
}
