using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BossProjectile : MonoBehaviour
{
    public float damage = 2f; // Schade die de projectile toebrengt aan de speler
    public float freezeDuration = 2f; // Duur van het bevriezingseffect

    void Start()
    {
        // Zorg ervoor dat de collider is ingesteld op "Is Trigger"
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;

        // Zorg ervoor dat de rigidbody correct is ingesteld
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("BossProjectile hit the player"); // Log the collision
            player.TakeDamage(2); // Apply damage to the player
            Destroy(gameObject); // Destroy the projectile
        }
    }
}

