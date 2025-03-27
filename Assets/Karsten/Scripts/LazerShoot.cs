using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LazerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform bulletSpawnpoint; // Het punt waar de kogel wordt gespawned
    public float bulletSpeed = 100.0f; // De snelheid van de kogel
    public float fireRate = 0.01f; // De tijd tussen het afvuren van kogels

    private bool isFiring = false;

    private bool button = false;

    void Update()
    {
        // Controleer of de rechtermuisknop is ingedrukt
        if (button)
        {
            if (!isFiring)
            {
                StartCoroutine(FireLaser());
            }
        }

        // Controleer of de rechtermuisknop is losgelaten
        if (button != true)
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

    public void OnSecondaryAttack(InputValue Value)
    {
         button = Value.Get<bool>();
    }
}
