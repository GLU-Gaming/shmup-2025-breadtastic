using System;
using UnityEngine;



public class EnemyBullet : MonoBehaviour
{
    public int damage = 2; // De schade die de kogel toebrengt

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"EnemyBullet collided with: {collision.gameObject.name}");

        // Controleer of de kogel de speler raakt
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("EnemyBullet hit the Player");

            // Breng schade toe aan de speler
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                int damage = 1; // Defineer de schade
                player.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage, remaining lives: {player.lives}");
            }

            // Vernietig de kogel
            Destroy(gameObject);
        }
    }
}
