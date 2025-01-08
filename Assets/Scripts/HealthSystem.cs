using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public GameObject explosionPrefab;

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
        if (_healthbar != null)
        {
            _healthbar.UpdateHealthBar(maxHealth, currentHealth);
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

        Destroy(gameObject);
    }
}
