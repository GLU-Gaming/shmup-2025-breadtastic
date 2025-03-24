using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 3; // Aantal levens van de speler

    public void TakeDamage(int damage)
    {
        lives -= 1; // Verminder het aantal levens met 1
        if (lives <= 0)
        {
            // Logica voor wanneer de speler sterft
            Debug.Log("Player is dead!");
        }
    }
}
