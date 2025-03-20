using System;
using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
    public int damage = 10; // De schade die de kogel toebrengt

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Controleer of de kogel de speler raakt
        if (collision.CompareTag("Player"))
        {
            // Breng schade toe aan de speler
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Vernietig de kogel
            Destroy(gameObject);
        }
    }
}
