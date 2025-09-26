using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private AudioClip respawnSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        spawner.Spawn(respawnSFX);
    }
}
