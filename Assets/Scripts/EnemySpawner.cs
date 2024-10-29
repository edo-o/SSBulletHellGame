using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject regularEnemyPrefab;
    public GameObject bossEnemyPrefab;
    public float spawnRadius = 1.5f;
    public Transform[] spawnPoints;
    public float spawnDelay = 2f;
    public int totalRegularEnemies = 5;

    private int enemiesSpawned = 0;
    private bool bossSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnRegularEnemies());
    }

    private IEnumerator SpawnRegularEnemies()
    {
        while (enemiesSpawned < totalRegularEnemies)
        {
            Transform validSpawnPoint = GetValidSpawnPoint();
            if (validSpawnPoint != null)
            {
                Instantiate(regularEnemyPrefab, validSpawnPoint.position, Quaternion.identity);
                enemiesSpawned++;
            }
            yield return new WaitForSeconds(spawnDelay);
        }

        SpawnBoss();
    }

    private Transform GetValidSpawnPoint()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Collider2D overlap = Physics2D.OverlapCircle(spawnPoint.position, spawnRadius);
            if (overlap == null)
            {
                return spawnPoint;
            }
        }
        return null;
    }

    private void SpawnBoss()
    {
        if (!bossSpawned)
        {
            Transform validSpawnPoint = GetValidSpawnPoint();
            if (validSpawnPoint != null)
            {
                Instantiate(bossEnemyPrefab, validSpawnPoint.position, Quaternion.identity);
                bossSpawned = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform spawnPoint in spawnPoints)
        {
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }
    }
    
}
