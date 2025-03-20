using System.Collections;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform bulletSpawnpoint; // Het punt waar de kogel wordt gespawned
    public float bulletSpeed = 100.0f; // De snelheid van de kogel
    public float fireRate = 0.01f; // De tijd tussen het afvuren van kogels voor de laser
    public float bulletCooldown = 0.5f; // De cooldown tijd voor het schieten met de linkermuisknop

    private bool isFiring = false;
    private float nextFireTime = 0f;

    void Update()
    {
        // Controleer of de linkermuisknop is ingedrukt en de cooldown is verstreken
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + bulletCooldown;
        }

        // Controleer of de rechtermuisknop is ingedrukt
        if (Input.GetMouseButtonDown(1))
        {
            if (!isFiring)
            {
                StartCoroutine(FireLaser());
            }
        }

        // Controleer of de rechtermuisknop is losgelaten
        if (Input.GetMouseButtonUp(1))
        {
            isFiring = false;
        }
    }

    IEnumerator FireLaser()
    {
        isFiring = true;

        while (isFiring)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void Shoot()
    {
        // Maak een nieuwe kogel aan op de positie van de BulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);

        // Voeg snelheid toe aan de kogel
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnpoint.right * bulletSpeed;
        }

        // Vernietig de kogel na 1.7 seconden
        Destroy(bullet, 1.7f);
    }

    void ShootBullet()
    {
        // Maak een nieuwe kogel aan op de positie van de BulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);

        // Voeg snelheid toe aan de kogel
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnpoint.right * bulletSpeed;
        }

        // Vernietig de kogel na 1.7 seconden
        Destroy(bullet, 1.7f);
    }
}
