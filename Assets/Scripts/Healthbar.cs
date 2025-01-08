using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    private float target = 1;
    [SerializeField] private float _fillSpeed = 2f;

    public void UpdateHealthBar (float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }

    void Update()
    {
        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, target, _fillSpeed * Time.deltaTime);
    }
}
