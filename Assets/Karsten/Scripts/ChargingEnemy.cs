using System;
using UnityEngine;

public class ChargingEnemy : Enemy
{
    public float chargeSpeed = 5.0f; // De snelheid waarmee de vijand naar de speler chargeert
    private Transform playerTransform;

    public static event Action<GameObject> OnEnemyDeath;

    void Start()
    {
        // Stel de huidige gezondheid in op een lagere waarde
        SetCurrentHealth(maxHealth / 2);

        // Zoek de speler in de scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
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

    private void SetCurrentHealth(int health)
    {
        typeof(Enemy).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(this, health);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Controleer of de vijand de speler raakt
        if (collision.CompareTag("Player"))
        {
            // Breng schade toe aan de speler
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1); // Verminder het aantal levens van de speler met 1
            }

            // Activeer de sterfgebeurtenis
            OnEnemyDeath?.Invoke(gameObject);

            // Vernietig de vijand
            Destroy(gameObject);
        }
    }
}
