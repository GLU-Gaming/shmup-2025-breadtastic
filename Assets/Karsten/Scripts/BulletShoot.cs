using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform bulletSpawnpoint; // Het punt waar de kogel wordt gespawned
    public float bulletSpeed = 10.0f; // De snelheid van de kogel

    void Update()
    {
        // Controleer of de linkermuisknop is ingedrukt
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
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
        Destroy(bullet, 1.0f);
    }
}
