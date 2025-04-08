using System.Collections.Generic;
using UnityEngine;

public class BossShotgunAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int projectileCount = 5;
    [SerializeField] private float spreadAngle = 45f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float freezeDuration = 2f; // Duration of the freeze effect

    private float nextFireTime;
    private bool useOpenSpaces = false; // Toggle between normal and open-space patterns
    private List<float> previousAngles = new List<float>(); // Store angles from the previous shot

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            ShootShotgun();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootShotgun()
    {
        float angleStep = spreadAngle / (projectileCount - 1);
        float angle = -spreadAngle / 2;

        List<float> currentAngles = new List<float>();

        for (int i = 0; i < projectileCount; i++)
        {
            // Alternate between normal and open-space patterns
            if (useOpenSpaces && previousAngles.Contains(angle))
            {
                angle += angleStep;
                continue; // Skip angles that were targeted in the previous shot
            }

            // Calculate the direction for each projectile
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 projectileDirection = rotation * firePoint.right;

            // Instantiate and set the velocity of the projectile
            GameObject tempProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, angle));
            tempProjectile.GetComponent<Rigidbody>().linearVelocity = projectileDirection * -10f;

            // Add the freeze effect to the projectile
            BossProjectile bossProjectile = tempProjectile.AddComponent<BossProjectile>();
            bossProjectile.freezeDuration = freezeDuration;

            // Destroy the projectile after 2 seconds
            Destroy(tempProjectile, 2f);

            // Store the current angle
            currentAngles.Add(angle);

            angle += angleStep;
        }

        // Update the previous angles and toggle the pattern
        previousAngles = currentAngles;
        useOpenSpaces = !useOpenSpaces;
    }
}
