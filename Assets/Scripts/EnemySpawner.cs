using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject regularEnemyPrefab;
    public GameObject bossEnemyPrefab;
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
            SpawnEnemy(regularEnemyPrefab);
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
        }

        SpawnBoss();
    }


    private void SpawnEnemy(GameObject enemyPrefab)
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }

    private void SpawnBoss()
    {
        if (!bossSpawned)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(bossEnemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            bossSpawned = true;
        }
    }
    
}
