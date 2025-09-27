using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private AudioClip respawnSFX;
    [SerializeField] private Net net;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnProjectile(collision);
    }

    private void RespawnProjectile(Collider2D collision)
    {
        Destroy(collision.gameObject);
        spawner.Spawn(respawnSFX);
        net.ResetValues();
        net.ResetStreak();
    }
}
