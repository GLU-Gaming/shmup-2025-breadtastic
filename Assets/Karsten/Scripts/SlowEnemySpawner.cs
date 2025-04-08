using UnityEngine;

public class SlowEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalEnemyPrefab; // The normal enemy prefab
    [SerializeField] private GameObject chargingEnemyPrefab; // The charging enemy prefab
    [SerializeField] private float spawnInterval = 10f; // Time between spawns
    [SerializeField] private Vector2 spawnAreaMin; // Minimum X and Y coordinates for the spawn area
    [SerializeField] private Vector2 spawnAreaMax; // Maximum X and Y coordinates for the spawn area

    private float nextSpawnTime;

    void Start()
    {
        // Set the initial spawn time
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // Check if it's time to spawn new enemies
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemies();
            nextSpawnTime = Time.time + spawnInterval; // Schedule the next spawn
        }
    }

    private void SpawnEnemies()
    {
        // Spawn 2 normal enemies
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy(normalEnemyPrefab);
        }

        // Spawn 2 charging enemies
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy(chargingEnemyPrefab);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Generate a random position within the spawn area
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // Instantiate the enemy at the random position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Debug.Log($"Spawned {enemyPrefab.name} at {spawnPosition}");
    }
}
