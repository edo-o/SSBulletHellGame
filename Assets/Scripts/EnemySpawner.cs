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
        
        UnityEngine.Vector2 targetPosition = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        enemy.GetComponent<EnemyMovement>().SetTargetPosition(targetPosition);
    }

    private void SpawnBoss()
    {
        if (!bossSpawned)
        {
            Vector2 spawnPosition = GetOffScreenPosition();
            GameObject bossEnemy = Instantiate(bossEnemyPrefab, spawnPosition, Quaternion.identity);

            Vector2 targetPosition = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            bossEnemy.GetComponent<EnemyMovement>().SetTargetPosition(targetPosition);

            bossSpawned = true;
        }
    }

    private Vector2 GetOffScreenPosition()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        int side = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: //Top
                spawnPosition = new Vector2(Random.Range(-camWidth, camWidth), camHeight + spawnDistance);
                break;
            case 2: //Left
                spawnPosition = new Vector2(-camWidth - spawnDistance, Random.Range(-camHeight, camHeight));
                break;
            case 3: //Right
                spawnPosition = new Vector2(camWidth + spawnDistance, Random.Range(-camHeight, camHeight));
                break;
        
        }
        return spawnPosition;
    }

    private void OnDrawGizmos()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f)), spawnRadius);
    }
    
}