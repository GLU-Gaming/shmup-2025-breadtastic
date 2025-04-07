using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damage = 10f; // Schade van de laser

    void OnTriggerEnter(Collider other)
    {
        // Controleer of de laser een vijand raakt
        Boss boss = other.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage); // Breng schade toe aan de boss
            Destroy(gameObject); // Vernietig de laser
        }
    }
}
