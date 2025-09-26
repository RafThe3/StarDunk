using UnityEngine;

public class Backboard : MonoBehaviour
{
    public bool isActive = false;

    private void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = isActive ? Color.yellow : Color.gray;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = true;
    }
}
