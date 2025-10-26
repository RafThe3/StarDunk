using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Backboard : MonoBehaviour
{
    [SerializeField] private Net net;
    [SerializeField] private Powerups powerups;
    [SerializeField] private AudioClip hitWhileActiveSFX;

    private bool isActive = false;

    private void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Light2D light2D = GetComponent<Light2D>();

        spriteRenderer.color = isActive ? Color.yellow : Color.gray;
        light2D.enabled = isActive;

        Debug.Log($"Is Active: {isActive}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
        {
            powerups.AddPoint();
        }
        else
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(hitWhileActiveSFX);
        }
        isActive = true;
        net.hitBackboard = true;
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
