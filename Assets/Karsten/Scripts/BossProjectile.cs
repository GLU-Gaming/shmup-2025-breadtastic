using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float damage = 2f; // Damage dealt by the projectile
    public float freezeDuration; // Duration to freeze the player

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"BossProjectile collided with: {collision.gameObject.name}");

        // Check if the projectile hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("BossProjectile hit the Player");

            // Damage and freeze the player
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage((int)damage); // Apply damage to the player
                player.Freeze(freezeDuration); // Freeze the player
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}

