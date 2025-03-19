using System.Collections;
using UnityEngine;

public class LazerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform bulletSpawnpoint; // Het punt waar de kogel wordt gespawned
    public float bulletSpeed = 100.0f; // De snelheid van de kogel
    public float fireRate = 0.01f; // De tijd tussen het afvuren van kogels

    private bool isFiring = false;

    void Update()
    {
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

        // Vernietig de kogel na 1 seconde
        Destroy(bullet, 1.7f);
    }
}
