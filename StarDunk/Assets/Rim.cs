using UnityEngine;

public class Rim : MonoBehaviour
{
    [SerializeField] private AudioClip hitSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(hitSFX);
    }
}
