using System.Collections;
using TMPro;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField] private float timeActive = 1;
    [SerializeField] private Backboard[] backboards;
    [SerializeField] private Projectile projectile;
    [SerializeField] private AudioClip[] hitSFX;
    [SerializeField] private TextMeshProUGUI powerupTimerText;

    private int points = 0;
    private float powerupTime = 0;
    private bool enablePowerups = false;

    private void Update()
    {
        enablePowerups = points == 4;

        if (enablePowerups)
        {
            //powerupTime += Time.deltaTime;
            StartCoroutine(EnableMultiJump());
            points = 0;
        }
    }

    public void AddPoint()
    {
        Debug.Log($"Added point. Points: {points}");

        points++;
        if (points - 1 < hitSFX.Length)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(hitSFX[points - 1]);
        }
    }

    private IEnumerator EnableMultiJump()
    {
        projectile.AllowMultiJump(true);
        yield return new WaitForSeconds(timeActive);
        projectile.AllowMultiJump(false);
        ResetBackboards();
    }

    private void ResetBackboards()
    {
        foreach (Backboard backboard in backboards)
        {
            backboard.SetActive(false);
        }
    }
}
