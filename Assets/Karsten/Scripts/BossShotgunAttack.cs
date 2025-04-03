using UnityEngine;

public class BossShotgunAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int projectileCount = 5;
    [SerializeField] private float spreadAngle = 45f;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float freezeDuration = 2f; // Duration of the freeze effect

    private float nextFireTime;

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

        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate the direction for each projectile with a 90-degree upward turn
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 projectileDirection = rotation * firePoint.right;

            // Instantiate and set the velocity of the projectile
            GameObject tempProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            tempProjectile.GetComponent<Rigidbody>().linearVelocity = projectileDirection * -10f;

            // Add the freeze effect to the projectile
            BossProjectile bossProjectile = tempProjectile.AddComponent<BossProjectile>();
            bossProjectile.freezeDuration = freezeDuration;

            // Destroy the projectile after 2 seconds
            Destroy(tempProjectile, 2f);

            angle += angleStep;
        }
    }
}
