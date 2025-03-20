using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.0f; // De snelheid van de vijand
    public int maxHealth = 100; // De maximale gezondheid van de vijand
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform bulletSpawnpoint; // Het punt waar de kogel wordt gespawned
    public float bulletSpeed = 10.0f; // De snelheid van de kogel
    public float fireRate = 2.0f; // De tijd tussen het afvuren van kogels

    private int currentHealth;
    private float nextFireTime = 0f;

    public event Action<GameObject> OnEnemyDeath; // Gebeurtenis die wordt geactiveerd wanneer de vijand sterft

    void Start()
    {
        // Stel de huidige gezondheid in op de maximale gezondheid
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Beweeg de vijand naar links
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Controleer of het tijd is om te schieten
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Maak een nieuwe kogel aan op de positie van de BulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);

        // Voeg snelheid toe aan de kogel in de linker richting
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = -bulletSpawnpoint.right * bulletSpeed;
        }

        // Vernietig de kogel na 2 seconden
        Destroy(bullet, 2.0f);
    }

    // Functie om schade toe te brengen aan de vijand
    public void TakeDamage(int damage)
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
        // Activeer de sterfgebeurtenis
        OnEnemyDeath?.Invoke(gameObject);

        // Vernietig de vijand
        Destroy(gameObject);
    }
}
