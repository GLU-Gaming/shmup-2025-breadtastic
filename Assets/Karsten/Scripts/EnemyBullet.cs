using System;
using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
    public int damage = 10; // De schade die de kogel toebrengt

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"EnemyBullet collided with: {collision.gameObject.name}");

        // Controleer of de kogel de speler raakt
        if (collision.CompareTag("Player"))
        {
            Debug.Log("EnemyBullet hit the Player");

            // Breng schade toe aan de speler
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage, remaining lives: {player.lives}");
            }

            // Vernietig de kogel
            Destroy(gameObject);
        }
    }
}
