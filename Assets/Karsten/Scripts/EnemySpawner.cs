
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // De prefab van de standaard vijand
    public GameObject chargingEnemyPrefab; // De prefab van de charging vijand
    public float spawnXPosition = 11.0f; // De X-positie waar de vijanden spawnen
    public float verticalSpacing = 2.0f; // De verticale afstand tussen de vijanden
    public float spawnAreaHeight = 10.0f; // De hoogte van het spawngebied
    public int initialEnemyCount = 3; // Het aantal vijanden in de eerste ronde

    private List<GameObject> enemies = new List<GameObject>(); // Lijst om de vijanden bij te houden
    private int currentRound = 1; // De huidige ronde

    void Start()
    {
        // Start de eerste ronde
        StartRound(initialEnemyCount);
    }

    void Update()
    {
        // Controleer of alle vijanden dood zijn
        if (enemies.Count == 0)
        {
            // Start een nieuwe ronde met meer vijanden
            StartRound(initialEnemyCount + currentRound);
            currentRound++;
        }
    }

    void StartRound(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (currentRound > 2 && i % 2 == 0) // Na 2 rondes, spawn elke tweede vijand als een charging vijand
            {
                SpawnChargingEnemy(i);
            }
            else
            {
                SpawnEnemy(i);
            }
        }
    }

    void SpawnEnemy(int index)
    {
        // Bepaal een willekeurige Y-positie binnen het spawngebied
        float spawnYPosition = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, 0);

        // Maak een nieuwe vijand aan op de spawnpositie
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Voeg de vijand toe aan de lijst
        enemies.Add(enemy);

        // Abonneer op het vijand sterfgebeurtenis
        enemy.GetComponent<Enemy>().OnEnemyDeath += HandleEnemyDeath;
    }

    void SpawnChargingEnemy(int index)
    {
        // Bepaal een willekeurige Y-positie binnen het spawngebied
        float spawnYPosition = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, 0);

        // Maak een nieuwe charging vijand aan op de spawnpositie
        GameObject enemy = Instantiate(chargingEnemyPrefab, spawnPosition, Quaternion.identity);

        // Voeg de vijand toe aan de lijst
        enemies.Add(enemy);

        // Abonneer op het vijand sterfgebeurtenis
        enemy.GetComponent<Enemy>().OnEnemyDeath += HandleEnemyDeath;
    }

    void HandleEnemyDeath(GameObject enemy)
    {
        // Verwijder de vijand uit de lijst wanneer deze sterft
        enemies.Remove(enemy);
    }
}
