using System.Runtime.CompilerServices;
using UnityEngine;

public class Rim : MonoBehaviour
{
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private Net net;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(hitSFX);
        net.hitRim = true;
    }
}
