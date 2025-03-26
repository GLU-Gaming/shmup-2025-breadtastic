using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 15; // Aantal levens van de speler

    private int damage = 1; // Hoeveelheid schade die de speler kan incasseren

    public void TakeDamage()
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
        Debug.Log($"EnemyBullet collided with: {collision.gameObject.name}");

        // Controleer of de kogel de speler raakt
        
            Debug.Log("EnemyBullet hit the Player");

            // Breng schade toe aan de speler
        EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {
            damage = enemyBullet.damage;
            TakeDamage();
        }


        // Breng schade toe aan de speler
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            damage = 1; // Verminder het aantal levens van de speler met 1
            TakeDamage();
        }

        // Vernietig de vijand
        Destroy(collision.gameObject);
    }
}
