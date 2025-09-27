using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Backboard : MonoBehaviour
{
    [SerializeField] private float activeLength = 1;
    [SerializeField] private Net net;

    private bool isActive = false;
    private float timeActive = 0;

    private void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Light2D light2D = GetComponent<Light2D>();

        if (isActive)
        {
            timeActive += Time.deltaTime;
        }

        if (timeActive >= activeLength)
        {
            isActive = false;
            timeActive = 0;
        }

        spriteRenderer.color = isActive ? Color.yellow : Color.gray;
        light2D.enabled = isActive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = true;
        net.hitBackboard = true;
    }
}
