using UnityEngine;



public class Bullet : MonoBehaviour
{
    public int damage = 10; // De schade die de kogel toebrengt

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Controleer of de kogel een vijand raakt
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Breng schade toe aan de vijand
            enemy.TakeDamage(damage);

            // Vernietig de kogel
            Destroy(gameObject);
        }
    }
}
