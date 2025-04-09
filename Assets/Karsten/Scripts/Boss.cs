using UnityEngine;
using UnityEngine.UI;

// Ensure the Boss object has a Collider component
[RequireComponent(typeof(Collider))]
public class Boss : MonoBehaviour
{
    public float health = 1000; // Total health of the boss
    public float maxHealth = 1000; // Maximum health of the boss
    public float damage = 1f; // Damage the boss deals to the player
    public GameObject chargingEnemyPrefab; // Prefab for the charging enemy
    public GameObject normalEnemyPrefab; // Prefab for the normal enemy
    public Transform spawnPoint; // Spawn point for the enemies
    public Slider healthBar; // Reference to the health bar UI

    private float lastHealthCheckpoint; // Tracks the last health checkpoint

    void Start()
    {
        // Ensure the collider is set to trigger
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;

        // Initialize the last health checkpoint
        lastHealthCheckpoint = health;

        // Initialize the health bar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Reduce the boss's health by the damage amount

        // Update the health bar
        if (healthBar != null)
        {
            healthBar.value = health;
        }

        // Check if the boss's health has dropped by 250 or more since the last checkpoint
        if (lastHealthCheckpoint - health >= 250)
        {
            SpawnEnemies(); // Spawn enemies
            lastHealthCheckpoint -= 250; // Update the health checkpoint
        }

        if (health <= 0)
        {
            Debug.Log("Boss is dead");
            Destroy(gameObject);
        }
    }

    private void SpawnEnemies()
    {
        Debug.Log("Spawning 2 charging enemies and 2 normal enemies");

        // Spawn 2 charging enemies
        for (int i = 0; i < 2; i++)
        {
            Instantiate(chargingEnemyPrefab, spawnPoint.position, Quaternion.identity);
        }

        // Spawn 2 normal enemies
        for (int i = 0; i < 2; i++)
        {
            Instantiate(normalEnemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Boss collided with: {collision.gameObject.name}");

        // Check if the boss is hit by a player bullet
        Bullet playerBullet = collision.GetComponent<Bullet>();
        if (playerBullet)
        {
            Debug.Log("Boss hit by PlayerBullet");
            TakeDamage(playerBullet.damage); // Reduce the boss's health by the bullet's damage
            Destroy(collision.gameObject); // Destroy the bullet
        }

        // Check if the boss is hit by a player laser
        Laser playerLaser = collision.GetComponent<Laser>();
        if (playerLaser)
        {
            Debug.Log("Boss hit by PlayerLaser");
            TakeDamage(playerLaser.damage); // Reduce the boss's health by the laser's damage
            Destroy(collision.gameObject); // Destroy the laser
        }

        // Check if the boss collides with the player
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            Debug.Log("Boss hit the Player");
            player.TakeDamage((int)damage); // Deal damage to the player
        }
    }
}
