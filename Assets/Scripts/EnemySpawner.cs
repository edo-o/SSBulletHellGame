using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject regularEnemyPrefab;
    public GameObject bossEnemyPrefab;
    public float spawnRadius = 1.5f;
    public float spawnDelay = 2f;
    public int totalRegularEnemies = 5;
    public float spawnDistance = 5f;

    private int enemiesSpawned = 0;
    private bool bossSpawned = false;
    private Camera mainCamera;

    public Vector2 targetPosition;

    void Start()
    {
        mainCamera = Camera.main;
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
        Vector2 spawnPosition = GetOffScreenPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        Vector2 targetPosition = GameObject.FindWithTag("TargetPosition").transform.position;
        enemy.GetComponent<EnemyMovement>().SetTargetPosition(targetPosition);
    }

    private void SpawnBoss()
    {
        if (!bossSpawned)
        {
            Vector2 spawnPosition = GetOffScreenPosition();
            GameObject bossEnemy = Instantiate(bossEnemyPrefab, spawnPosition, Quaternion.identity);

            Vector2 targetPosition = GameObject.FindWithTag("TargetPosition").transform.position;
            bossEnemy.GetComponent<EnemyMovement>().SetTargetPosition(targetPosition);

            bossSpawned = true;
        }
    }

    private Vector2 GetOffScreenPosition()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;


        // Få enemies til å spawne på høyre og venstre side av skjermen
        Vector2 spawnPosition = new Vector2(Random.Range(-camWidth, camWidth), camHeight + spawnDistance);

        return spawnPosition;
    }
    
}