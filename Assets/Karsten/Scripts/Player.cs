using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int lives = 15; // Aantal levens van de speler
    private OnDead onDead;
    public bool Isdead = true;


    private void Start()
    {
        onDead = FindFirstObjectByType<OnDead>();
    }

    public void TakeDamage(int damage)
    {
        lives -= damage; // Verminder het aantal levens met de hoeveelheid schade
        if (lives <= 0 && onDead)
        {
            Debug.Log("dead");
            onDead.Dead(Isdead);
            Destroy(gameObject);
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
