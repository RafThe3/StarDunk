using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Backboard : MonoBehaviour
{
    [SerializeField] private bool isActive = false;
    [SerializeField] private Net net;

    private void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Light2D light2D = GetComponent<Light2D>();

        spriteRenderer.color = isActive ? Color.yellow : Color.gray;
        light2D.enabled = isActive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = true;
        net.hitBackboard = true;
    }
}
