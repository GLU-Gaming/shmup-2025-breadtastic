using UnityEngine;

public class FastShootingEnemy : Enemy
{


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

    public new void Shoot()
    {
        // Maak een nieuwe kogel aan op de positie van de EnemyBulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, EnemyBulletSpawnpoint.position, Quaternion.Euler(0, 0, -90));

        // Voeg snelheid toe aan de kogel in de voorwaartse richting van de EnemyBulletSpawnpoint
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = -EnemyBulletSpawnpoint.right * bulletSpeed;
        }

        // Vernietig de kogel na 2 seconden
        Destroy(bullet, 4.0f);
    }
}
