using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.0f; // De snelheid van de vijand
    public float verticalSpeed = 1.0f; // De verticale snelheid van de vijand
    public float verticalAmplitude = 1.0f; // De amplitude van de verticale beweging
    public float maxHealth = 50f; // De maximale gezondheid van de vijand
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform EnemyBulletSpawnpoint; // Het punt waar de kogel wordt gespawned
    public float bulletSpeed = 5.0f; // De snelheid van de kogel
    public float minFireRate = 1.0f; // De minimale tijd tussen het afvuren van kogels
    public float maxFireRate = 3.0f; // De maximale tijd tussen het afvuren van kogels
    public int scoreValue = 25; // De scorewaarde van de vijand

    public float currentHealth;
    public float nextFireTime = 0f;
    public float initialYPosition;

    public event Action<GameObject> OnEnemyDeath; // Gebeurtenis die wordt geactiveerd wanneer de vijand sterft

    void Start()
    {
        // Stel de huidige gezondheid in op de maximale gezondheid
        currentHealth = maxHealth;
        initialYPosition = transform.position.y;
        SetNextFireTime();
    }

    void Update()
    {
        // Beweeg de vijand naar links en voeg verticale beweging toe
        float newYPosition = initialYPosition + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, newYPosition, transform.position.z);

        // Controleer of het tijd is om te schieten
        if (Time.time >= nextFireTime)
        {
            Shoot();
            SetNextFireTime();
        }
    }

    void SetNextFireTime()
    {
        nextFireTime = Time.time + UnityEngine.Random.Range(minFireRate, maxFireRate);
    }

    public void Shoot()
    {
        // Maak een nieuwe kogel aan op de positie van de EnemyBulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, EnemyBulletSpawnpoint.position, Quaternion.Euler(0, 0, -90));

        // Voeg snelheid toe aan de kogel in de voorwaartse richting van de EnemyBulletSpawnpoint
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = EnemyBulletSpawnpoint.right * -bulletSpeed;
        }

        // Vernietig de kogel na 2 seconden
        Destroy(bullet, 4.0f);
    }

    // Functie om schade toe te brengen aan de vijand
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Controleer of de vijand dood is
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Functie om de vijand te vernietigen
    void Die()
    {
        // Add score when the enemy dies
        ScoreManager.instance.AddScore(scoreValue);

        // Activeer de sterfgebeurtenis
        OnEnemyDeath?.Invoke(gameObject);

        // Vernietig de vijand
        Destroy(gameObject);
    }
}
