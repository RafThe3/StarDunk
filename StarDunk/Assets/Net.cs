using UnityEngine;
using TMPro;

public class Net : MonoBehaviour
{
    [SerializeField] private int baseScore = 1;
    [SerializeField] private int multiplier = 1;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Spawner spawner;
    [SerializeField] private AudioClip scoreSFX;

    private int score = 0;

    private void Update()
    {
        scoreText.text = $"Score: {score}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(scoreSFX);
        score += baseScore * multiplier;
        Destroy(collision.gameObject);
        spawner.Spawn();
    }
}
