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
}
