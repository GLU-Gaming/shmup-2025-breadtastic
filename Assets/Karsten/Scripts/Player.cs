using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 15; // Aantal levens van de speler

    public void TakeDamage(int damage)
    {
        lives -= damage; // Verminder het aantal levens met de hoeveelheid schade
        if (lives <= 0)
        {
            // Logica voor wanneer de speler sterft
            Debug.Log("Player is dead!");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Player collided with: {collision.gameObject.name}");

        // Controleer of de speler een vijandelijke kogel raakt
        EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
        if (enemyBullet)
        {
            Debug.Log("Player hit by Enemy");
            TakeDamage(enemyBullet.damage); // Verminder het aantal levens van de speler met 1
            Destroy(collision.gameObject); // Vernietig de vijand
        }

        // Controleer of de speler een vijand raakt
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("Player hit by Enemy");
            TakeDamage(1); // Verminder het aantal levens van de speler met 1
            Destroy(collision.gameObject); // Vernietig de vijand
        }
    }    
}
