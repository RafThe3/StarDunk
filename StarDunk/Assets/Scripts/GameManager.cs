using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool enableGameTimer = false;
    [SerializeField] private int gameLengthInSeconds = 120;
    [SerializeField] private TextMeshProUGUI gameTimerText;

    private float seconds;
    private int minutes;

    private void Start()
    {
        minutes = gameLengthInSeconds / 60;
        seconds = gameLengthInSeconds % 60;
    }

    private void Update()
    {
        ReloadGame();
        SetTimer();
    }

    private void SetTimer()
    {
        if (!enableGameTimer)
        {
            return;
        }

        seconds -= Time.deltaTime;

        if (seconds <= 0)
        {
            minutes--;
            seconds += 60;

            if (minutes <= 0)
            {
                minutes = 0;
                seconds = 0;
                return;
            }
        }

        gameTimerText.text = $"{minutes}:{(int)seconds:00}";
    }

    private static void ReloadGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
