using System;
using UnityEngine;

public class ChargingEnemy : Enemy
{
    public float chargeSpeed = 5.0f; // De snelheid waarmee de vijand naar de speler chargeert
    private Transform playerTransform;

    new public static event Action<GameObject> OnEnemyDeath;

    void Start()
    {
        // Stel de huidige gezondheid in op de maximale gezondheid
        SetCurrentHealth(maxHealth);

        // Zoek de speler in de scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Player found and playerTransform set.");
        }
        else
        {
            Debug.LogError("Player not found. Ensure the player GameObject is tagged as 'Player'.");
        }
    }

    void Update()
    {
        // Beweeg de vijand naar de speler toe
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * chargeSpeed * Time.deltaTime);
        }
    }

    private void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }

    void OnTriggerEnter(Collider collision)
    {
        // Controleer of de vijand de speler raakt
        if (collision.CompareTag("Player"))
        {
            // Breng schade toe aan de speler
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(10000); // Verminder het aantal levens van de speler met 1
            }

            // Activeer de sterfgebeurtenis
            OnEnemyDeath?.Invoke(gameObject);

            // Vernietig de vijand
            Destroy(gameObject);
        }
    }
}
