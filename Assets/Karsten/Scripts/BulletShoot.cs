using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;




public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform bulletSpawnpoint; // Het punt waar de kogel wordt gespawned
    public float bulletSpeed = 100.0f; // De snelheid van de kogel
    public float laserSpeed = 150.0f; // De snelheid van de laser
    public float fireRate = 0.01f; // De tijd tussen het afvuren van kogels voor de laser
    public float bulletCooldown = 0.5f; // De cooldown tijd voor het schieten met de linkermuisknop
    public float laserCooldown = 10.0f; // De cooldown tijd voor de laser
    public float laserDuration = 2.0f; // De tijd dat de laser kan worden gebruikt

    public Image laserCooldownImage; // UI-element voor de laser cooldown

    private bool isFiring = false;
    private float nextFireTime = 0f;
    private float nextLaserTime = 0f;

    private float buttonMain = 0;
    private float buttonSecondary = 0;

    public Rumble rumble;

    private void Start()
    {
        rumble = FindFirstObjectByType<Rumble>();
    }


    void Update()
    {
        // Controleer of de linkermuisknop is ingedrukt en de cooldown is verstreken
        if (buttonMain != 0 && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + bulletCooldown;
        }



        // Controleer of de rechtermuisknop is ingedrukt en de laser cooldown is verstreken
        if (buttonSecondary != 0 && Time.time >= nextLaserTime)


            // Controleer of de rechtermuisknop is ingedrukt
            if (buttonSecondary != 0)

            {
                if (!isFiring)
                {
                    StartCoroutine(FireLaser());
                }
            }

        // Controleer of de rechtermuisknop is losgelaten
        if (buttonSecondary == 0)
        {
            isFiring = false;
        }
    }

    IEnumerator FireLaser()
    {
        isFiring = true;
        nextLaserTime = Time.time + laserCooldown;

        float laserEndTime = Time.time + laserDuration;
        while (isFiring && Time.time < laserEndTime)
        {
            ShootLaser();
            yield return new WaitForSeconds(fireRate);
        }

        isFiring = false;
    }

    void Shoot()
    {
        // Maak een nieuwe kogel aan op de positie van de BulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, Quaternion.Euler(0, 0, -90));

        // Voeg snelheid toe aan de kogel
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
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
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, Quaternion.Euler(0, 0, -90));
        if (rumble)
        {
            rumble.StartRumble(0.3f, 0f, 0.2f, 1);
        }

        // Voeg snelheid toe aan de kogel
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnpoint.right * bulletSpeed;
        }

        // Vernietig de kogel na 1.7 seconden
        Destroy(bullet, 2.5f);
    }

    void ShootLaser()
    {
        // Maak een nieuwe kogel aan op de positie van de BulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, Quaternion.Euler(0, 0, -90));
        if (rumble)
        {
            rumble.StartRumble(0.3f, 0.1f, 0.2f, 1);
        }

        // Voeg snelheid toe aan de kogel
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnpoint.right * laserSpeed;
        }

        // Vernietig de kogel na 1.7 seconden
        Destroy(bullet, 2.5f);
    }

    public void OnMianAttack(InputValue value)
    {
        buttonMain = value.Get<float>();
    }

    public void OnAttack(InputValue Value)
    {
        buttonSecondary = Value.Get<float>();
    }    
}
