using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private bool enableGameTimer = false;
    [SerializeField] private int gameLengthInSeconds = 120;
    [SerializeField] private TextMeshProUGUI gameTimerText;

    private float seconds;
    private int minutes;

    public void Awake()
    {
        if (instance)
        {
            Debug.LogError("More than one instance of Game Manager found!");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
        bool isGameOver = seconds <= 0 && minutes <= 0;
        //Debug.Log($"Is Game Over: {isGameOver}");
        //Debug.Log($"Minutes: {minutes}, Seconds: {seconds}");

        if (!enableGameTimer || isGameOver)
        {
            seconds = 0;
            minutes = 0;
            SceneManager.LoadScene(0);
            return;
        }

        seconds -= Time.deltaTime;

        if (seconds <= 0 && minutes > 0)
        {
            minutes--;
            seconds += 60;
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

    public void LoadAScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void LoadAScene(string buildName)
    {
        SceneManager.LoadScene(buildName);
    }
}
