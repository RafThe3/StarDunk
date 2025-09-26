using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject spawnPrefab;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int spawn = Random.Range(0, spawnPoints.Length);
        Instantiate(spawnPrefab, spawnPoints[spawn].position, Quaternion.identity);
    }

    public void Spawn(AudioClip audioClip)
    {
        int spawn = Random.Range(0, spawnPoints.Length);
        Instantiate(spawnPrefab, spawnPoints[spawn].position, Quaternion.identity);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }
}
