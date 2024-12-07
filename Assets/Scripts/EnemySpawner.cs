using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject bossEnemyPrefab;
    public float spawnDelay = 2f;
    public float spawnDistance = 5f;

    private bool bossSpawned = false;
    private Camera mainCamera;

    public Vector2 targetPosition;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnBosses());
    }

    private IEnumerator SpawnBosses()
    {
        while (true)
        {
            if (!bossSpawned)
            {
                SpawnBoss();
                bossSpawned = true;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private void SpawnBoss()
    {
        if (!bossSpawned)
        {
            Vector2 spawnPosition = GetOffScreenPosition();
            GameObject bossEnemy = Instantiate(bossEnemyPrefab, spawnPosition, Quaternion.identity);

            Vector2 targetPosition = GameObject.FindWithTag("TargetPosition").transform.position;
            bossEnemy.GetComponent<EnemyMovement>().SetTargetPosition(targetPosition);
        }
    }

    private Vector2 GetOffScreenPosition()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;


        // Randomly choose to spawn on the left or right side
        bool spawnOnLeft = Random.value > 0.5f;
        float xPosition = spawnOnLeft ? -camWidth - spawnDistance : camWidth + spawnDistance;
        float yPosition = Random.Range(-camHeight, camHeight);

        return new Vector2(xPosition, yPosition);
    }
    
}