using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool levelWon = false;
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
