using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public GameObject explosionPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (gameObject.CompareTag("Boss"))
        {
            GameManager.instance.BossDefeated();
        }

        Destroy(gameObject);
    }
}
