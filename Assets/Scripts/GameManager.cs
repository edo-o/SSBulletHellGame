using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For reloading scenes
using UnityEngine.UI; // For managing UI elements

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public HealthSystem playerHealth;
    public HealthSystem bossHealth;

    public static GameManager instance;
    private bool levelWon = false;

    private void Update()
    {
        if (bossHealth != null && bossHealth.currentHealth <= 0)
        {
            WinGame();
        }
        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void LoseGame()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        Time.timeScale = 1; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BossDefeated()
    {
        if (!levelWon)
        {
            levelWon = true;
            Debug.Log("Level Complete!");
        }
    }
}
