using UnityEngine;



public class Bullet : MonoBehaviour
{
    public float damage = 10f; // De schade die de kogel toebrengt

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"Bullet collided with: {collision.gameObject.name}");

        // Controleer of de kogel een vijand raakt
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Controleer of de kogel een boss raakt
        Boss boss = collision.gameObject.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Bullet triggered with: {other.gameObject.name}");

        // Controleer of de kogel een vijand raakt
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Controleer of de kogel een boss raakt
        Boss boss = other.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
