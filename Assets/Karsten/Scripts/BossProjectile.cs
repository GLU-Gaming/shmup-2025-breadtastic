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
        // Controleer of de projectile de speler raakt
        // Controleer of de projectile de speler raakt
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("BossProjectile hit the Player"); // Debug log
            player.TakeDamage((int)damage); // Breng schade toe aan de speler
            player.Freeze(freezeDuration); // Bevries de speler
            Destroy(gameObject); // Vernietig de projectile
        }
    }
}

