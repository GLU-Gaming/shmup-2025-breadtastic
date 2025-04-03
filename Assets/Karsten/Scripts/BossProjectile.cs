using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float freezeDuration;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"BossProjectile collided with: {collision.gameObject.name}");

        // Check if the projectile hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("BossProjectile hit the Player");

            // Freeze the player
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Freeze(freezeDuration);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}

