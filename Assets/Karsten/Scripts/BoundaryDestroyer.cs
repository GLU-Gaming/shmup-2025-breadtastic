using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour
{
    private EnemySpawner enemySpawner;

    void Start()
    {
        // Find the EnemySpawner in the scene
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Notify the EnemySpawner to remove the enemy from the list
            enemySpawner.RemoveEnemy(other.gameObject);

            // Add score when the enemy is destroyed
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                ScoreManager.instance.AddScore(enemy.scoreValue);
            }

            // Destroy the enemy
            Destroy(other.gameObject);
        }
    }
}
