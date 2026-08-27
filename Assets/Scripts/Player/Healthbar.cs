using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private bool _useSpriteStates;
    [SerializeField] private Sprite[] _healthSprites;

    private float target = 1;
    [SerializeField] private float _fillSpeed = 2f;

    public void UpdateHealthBar (float maxHealth, float currentHealth)
    {
        float healthPercentage = currentHealth / maxHealth;

        if (_useSpriteStates)
        {
            int index = Mathf.RoundToInt(healthPercentage * 4);
            _healthbarSprite.sprite = _healthSprites[index];
        }
        else
        {
            target = healthPercentage;
        }
        
    }

    void Update()
    {
        if (!_useSpriteStates)
        {
            _healthbarSprite.fillAmount = Mathf.MoveTowards(
                _healthbarSprite.fillAmount,
                target,
                _fillSpeed * Time.deltaTime
                );
        }
    }

   
}
