using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Spiral, Wave, Explosion, TripleShot }

    [Header("Bullet Attributes")]
    public float bulletLife;
    public float speed; 
    private GameObject player;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private int explosionBulletCount = 8;
    [SerializeField] private GameObject smallBullet;
    [SerializeField] private GameObject mediumBullet;
    private float firingRate;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float spiralAngle = 0f;
    private float waveAngle = 0f;

    [Header("Randomization Attributes")]
    public float changeAttackInterval = 5f;
    private float attackChangeTimer = 0f;

    private HealthSystem bossHealth;
    private BossMovement bossMovement;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        bossHealth = GetComponent<HealthSystem>();
        bossMovement = GetComponent<BossMovement>();
    }

    
    void Update()
    {
       timer += Time.deltaTime;
       attackChangeTimer += Time.deltaTime;

       if (attackChangeTimer >= changeAttackInterval)
       {
              RandomizeAttackPattern();
              attackChangeTimer = 0f;
       }

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

    private void RandomizeAttackPattern()
    {
        List<SpawnerType> validPatterns = new List<SpawnerType>();

        if ( bossMovement.currentPhase == BossMovement.BossPhase.Phase1)
        {
            validPatterns.Add(SpawnerType.Straight);
            validPatterns.Add(SpawnerType.Spiral);
            validPatterns.Add(SpawnerType.TripleShot);
        }
        else if (bossMovement.currentPhase == BossMovement.BossPhase.Phase2)
        {
            validPatterns.Add(SpawnerType.Wave);
            validPatterns.Add(SpawnerType.Explosion);
            validPatterns.Add(SpawnerType.TripleShot);
        }

        int patternCount = validPatterns.Count;
        spawnerType = validPatterns[Random.Range(0, patternCount)];
    }

    private void Fire()
    {
        GameObject bulletPrefab = null;

        if (spawnerType == SpawnerType.Spiral)
        {
            bulletPrefab = smallBullet;
        }
        else
        {
            bulletPrefab = mediumBullet;
        }
        
        if(bulletPrefab)
        {
            spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            if (spawnerType == SpawnerType.Straight && player != null)
            {
                firingRate = 0.4f;
                
                Vector2 direction = (player.transform.position - transform.position).normalized;
                spawnedBullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
            else if (spawnerType == SpawnerType.Spiral)
            {
                firingRate = 0.1f;

                float angle = spiralAngle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                spawnedBullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
                spiralAngle += 10f;
            }
            else if (spawnerType == SpawnerType.Wave)
            {
                firingRate = 0.05f;

                float angle = Mathf.Sin(waveAngle * Mathf.Deg2Rad) * 45f;
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                spawnedBullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
                waveAngle += 10f;
            }
            else if (spawnerType == SpawnerType.TripleShot)
            {
                firingRate = 1f;
                float[] angles = { -15, 0f, 15f };

                Vector2 baseDirection = (player.transform.position - transform.position).normalized;
                float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

                foreach (float angle in angles)
                {
                    float radian = (baseAngle + angle) * Mathf.Deg2Rad;
                    Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
                    spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    spawnedBullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
                    spawnedBullet.GetComponent<Bullet>().speed = speed;
                    spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                }
            }
            else if (spawnerType == SpawnerType.Explosion)
            {
                for (int i = 0; i < explosionBulletCount; i++)
                {
                    firingRate = 0.5f;

                    float angle = i * (360 / explosionBulletCount);
                    Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                    spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    spawnedBullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
                    spawnedBullet.GetComponent<Bullet>().speed = speed;
                    spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                }
            }
            else
            {
                spawnedBullet.transform.rotation = transform.rotation;
                spawnedBullet.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            }
        }
    }
}


//straight bullet: Fire rate = 0.4, Bullet size = medium.
//spiral bullet: Fire rate = 0.1 , Bullet size = small.
//wave bullet: Fire rate = 0.05, Bullet size = medium.
//explosion bullet: Fire rate = 0.5, bullet size = medium.