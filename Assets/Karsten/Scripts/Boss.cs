using UnityEngine;

// Ensure the Boss object has a Collider component
[RequireComponent(typeof(Collider))]
public class Boss : MonoBehaviour
{
    public float health = 100; // Aantal levens van de boss

    void Start()
    {
        // Ensure the collider is set to trigger
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Verminder het aantal levens met de hoeveelheid schade
        if (health <= 0)
        {
            Debug.Log("Boss is dead");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Boss collided with: {collision.gameObject.name}");

        // Controleer of de boss een speler kogel raakt
        Bullet playerBullet = collision.GetComponent<Bullet>();
        if (playerBullet)
        {
            Debug.Log("Boss hit by PlayerBullet");
            TakeDamage(playerBullet.damage); // Verminder het aantal levens van de boss met de schade van de kogel
            Destroy(collision.gameObject); // Vernietig de kogel
        }
    }
}
