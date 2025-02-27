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

    private void Start()
    {
        // Find the boss GameObject by its tag
        GameObject boss = GameObject.FindWithTag("Boss");
        if (boss != null)
        {
            bossHealth = boss.GetComponent<HealthSystem>();
        }

        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    void WinGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0; // Stop time
        Debug.Log("WinGame called: Win screen should be active.");
    }

    void LoseGame()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
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
            WinGame();
        }
    }

    public void PlayerDefeated()
    {
        if (!levelWon)
        {
            levelWon = false;
            Debug.Log("Player Defeated!");
            LoseGame();
        }
    }
}




