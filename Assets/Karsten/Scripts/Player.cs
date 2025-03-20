using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Logica voor wanneer de speler sterft
            Debug.Log("Player is dead!");
        }
    }
}
