using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public GameObject explosionPrefab;
    public GameManager LoseGame;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [SerializeField] private Healthbar _healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (_healthbar != null)
        {
            _healthbar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current Health after damage: " + currentHealth);
        if (_healthbar != null)
        {
            _healthbar.UpdateHealthBar(maxHealth, currentHealth);
            Debug.Log("Healthbar updated in TakeDamage.");
        }
        else
        {
            Debug.LogError("Healthbar reference is not set in the Inspector.");
        }

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = originalColor;

        if (_healthbar != null)
        {
            _healthbar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }

    void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (gameObject.CompareTag("Boss"))
        {
            GameManager.instance.BossDefeated();
        }

        if (gameObject.CompareTag("Player"))
        {
            GameManager.instance.PlayerDefeated();
        }

        Destroy(gameObject);
    }
}




