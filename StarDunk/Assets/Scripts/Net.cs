using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class Net : MonoBehaviour
{
    [SerializeField] private int baseScore = 1;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Spawner spawner;
    [SerializeField] private AudioClip baseScoreSFX;
    [SerializeField] private AudioClip cleanShotSFX;
    [SerializeField] private AudioClip extraCleanShotSFX;
    [SerializeField] private TextMeshProUGUI basketTypeText;
    [SerializeField] private GameManager gameManager;

    [HideInInspector] public bool hitBackboard = false;
    [HideInInspector] public bool hitRim = false;

    private int score = 0;
    private bool basket, cleanShot, extraCleanShot;
    private int multiplier = 1;
    private int streak = 0;

    private void Start()
    {
        basketTypeText.enabled = false;
    }

    private void Update()
    {
        basket = hitBackboard;
        cleanShot = !hitBackboard && hitRim;
        extraCleanShot = !(hitBackboard || hitRim);

        multiplier = basket ? 1 : cleanShot ? 2 : 3;

        if (streak > PlayerPrefs.GetInt("Streak"))
        {
            PlayerPrefs.SetInt("Streak", streak);
        }
        
        scoreText.text = $"Score: {score}\nHigh Score: {PlayerPrefs.GetInt("Score")}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioClip clip = basket ? baseScoreSFX : cleanShot ? cleanShotSFX : extraCleanShotSFX;

        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
        CheckStreak();
        score += baseScore * multiplier;
        PlayerPrefs.SetInt("Score", score);
        StartCoroutine(DisplayBasketType());
        Destroy(collision.gameObject);
        spawner.Spawn();
        ResetValues();

        /* for multi-ball
        if (!collision.CompareTag("Clone"))
        {
            Destroy(collision.gameObject);
            spawner.Spawn();
        }
        else
        {
            collision.GetComponent<Projectile>().CanShoot(false);
        }
        ResetValues();
        */
    }

    public void ResetValues()
    {
        hitBackboard = false;
        hitRim = false;
    }

    private IEnumerator DisplayBasketType()
    {
        float duration = 1.5f;

        basketTypeText.enabled = true;
        basketTypeText.text = basket ? $"Basket\n{baseScore} x {multiplier}" : cleanShot ? $"Clean Shot\n{baseScore} x {multiplier}" : $"Swish\n{baseScore} x {multiplier}\nStreak: {streak}";
        yield return new WaitForSeconds(duration);
        basketTypeText.enabled = false;
    }

    private void CheckStreak()
    {
        if (extraCleanShot)
        {
            streak++;
        }
        else
        {
            streak = 0;
        }

        if (streak > 1)
        {
            multiplier += streak - 1;
        }
    }

    public void ResetStreak()
    {
        streak = 0;
    }
}
