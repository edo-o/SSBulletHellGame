using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Circle }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife;
    public float speed; 

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private int bulletCount = 8;
    [SerializeField] private float rotationSpeed = 30f;


    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(spawnerType == SpawnerType.Spin)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        if(timer >= firingRate) 
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if(bullet = null)return;

        if(spawnerType == SpawnerType.Circle)
        {
            float angleStep = 360f / bulletCount;
            float angle = 0f;

            for(int i = 0; i < bulletCount; i++)
            {
                float bulletDirX = Mathf.Cos((angle * Mathf.PI) / 180f);
                float bulletDirY = Mathf.Sin((angle * Mathf.PI) / 180f);

                Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
                Vector2 bulletDir = bulletMoveVector.normalized;

                GameObject spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                spawnedBullet.GetComponent<Rigidbody2D>().velocity = bulletDir * speed;

                float bulletAngle = Mathf.Atan2(bulletDirY, bulletDirX) * Mathf.Rad2Deg;
                spawnedBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, bulletAngle));

                angle += angleStep;
            }
        }
        else if (spawnerType == SpawnerType.Spin || spawnerType == SpawnerType.Straight)
        {
            GameObject spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = transform.position * speed;
        }
    }
}
