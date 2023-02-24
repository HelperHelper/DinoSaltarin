using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { get; protected set; }
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void GameOverPlayer()
    {
        gameObject.SetActive(true);

    }

    public void Retry()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
