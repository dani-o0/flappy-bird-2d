using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab del obstáculo
    public float spawnInterval = 2f;  // Tiempo entre spawns
    public float heightVariance = 2f; // Variación vertical de los obstáculos

    private float timer;

    void Update()
    {
        if (!GameManager.Instance.IsPlaying())
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        float randomHeight = Random.Range(-heightVariance, heightVariance);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomHeight, 0);

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}