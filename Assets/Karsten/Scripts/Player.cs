using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int lives = 15; // Aantal levens van de speler
    private OnDead onDead;
    public bool Isdead = true;
    private bool isFrozen = false;
    private float freezeTimer = 0f;

    // Toegevoegd voor hit overlay
    public Image hitOverlay; // Reference to the UI Image component for the hit overlay
    public float overlayDuration = 0.5f; // Duration for which the overlay is visible
    private float overlayTimer;

    private void Start()
    {
        onDead = FindFirstObjectByType<OnDead>();

        // Zorg ervoor dat de overlay aanvankelijk is uitgeschakeld
        if (hitOverlay != null)
        {
            hitOverlay.enabled = false;
        }
    }

    private void Update()
    {
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                isFrozen = false;
            }
        }

        // Beheer de overlay-timer
        if (overlayTimer > 0)
        {
            overlayTimer -= Time.deltaTime;
            if (overlayTimer <= 0 && hitOverlay != null)
            {
                hitOverlay.enabled = false; // Schakel de overlay uit wanneer de timer verloopt
            }
        }
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

        // Toon de hit overlay wanneer de speler schade oploopt
        ShowHitOverlay();
    }

    public void Freeze(float duration)
    {
        isFrozen = true;
        freezeTimer = duration;
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

        // Controleer of de speler een boss projectile raakt
        BossProjectile bossProjectile = collision.GetComponent<BossProjectile>();
        if (bossProjectile)
        {
            Debug.Log("Player hit by BossProjectile");
            TakeDamage(2); // Verminder het aantal levens van de speler met 2
            Freeze(bossProjectile.freezeDuration); // Bevries de speler
            Destroy(collision.gameObject); // Vernietig de boss projectile
        }
    }

    // Toegevoegd voor hit overlay
    private void ShowHitOverlay()
    {
        if (hitOverlay != null)
        {
            hitOverlay.enabled = true; // Schakel de overlay in
            overlayTimer = overlayDuration; // Reset de timer
        }
    }
}
