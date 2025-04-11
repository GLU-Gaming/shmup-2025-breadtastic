using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public int lives = 15; // Aantal levens van de speler
    public OnDead onDead; // Reference to the OnDead script
    public Helt playerHP; // Reference to the PlayerHP script
    public bool Isdead = true; // Boolean to track if the player is dead
    private bool isFrozen = false; // Boolean to track if the player is frozen
    private float freezeTimer = 0f; // Timer for the freeze effect
    public float speed = 5f; // Snelheid van de speler

    // Toegevoegd voor hit overlay
    public Image hitOverlay; // Reference to the UI Image component for the hit overlay
    public float overlayDuration = 0.5f; // Duration for which the overlay is visible
    private float overlayTimer; // Timer for the hit overlay



    public void Start()
    {
        onDead = FindFirstObjectByType<OnDead>(); // Find the OnDead script in the scene
        playerHP = GetComponent<Helt>(); // Get the PlayerHP component attached to the player

        // Zorg ervoor dat de overlay aanvankelijk is uitgeschakeld
        if (hitOverlay != null)
        {
            hitOverlay.enabled = false; // Disable the hit overlay at the start
        }

        // Zorg ervoor dat de rigidbody correct is ingesteld
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Ensure the Rigidbody is not kinematic

        // Initialize the health bar
        if (playerHP != null)
        {
            // Corrected the method call to match the Helt class implementation
            playerHP.HPBar(lives); // Sync the health bar with the player's lives
        }


    }

    private void Update()
    {
        // Handle the freeze effect timer
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                isFrozen = false; // Unfreeze the player when the timer ends
            }
        }

        // Beheer de overlay-timer
        if (overlayTimer > 0)
        {
            overlayTimer -= Time.deltaTime;
            if (overlayTimer <= 0 && hitOverlay != null)
            {
                hitOverlay.enabled = false; // Disable the overlay when the timer ends
            }
        }
    }

    public void TakeDamage(int damage, bool Display = true)
    {
        Debug.Log($"Player took {damage} damage"); // Log the damage taken
        lives -= damage; // Reduce the player's lives by the damage amount
        lives = Mathf.Max(lives, 0); // Ensure lives do not go below 0

        if (playerHP != null)
        {
            playerHP.HPBar(lives, Display); // Sync the health bar with the player's lives & Update the health bar;
            audioManager.instance.PlayPlayerHitSound();
        }

        if (lives <= 0)
        {
            audioManager.instance.PlayPlayerDeathSound();
            Debug.Log("Player is dead. Calling OnDead.Dead method."); // Log that the player is dead
            if (onDead != null)
            {
                onDead.Dead(true); // Notify the OnDead script that the player is dead
            }
            Destroy(gameObject); // Destroy the player object
        }
    }

    public void Freeze(float duration)
    {
        isFrozen = true; // Set the player to frozen
        freezeTimer = duration; // Set the freeze timer
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"Player collided with: {collision.gameObject.name}"); // Log the collision

        // Controleer of de speler een vijandelijke kogel raakt
        EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
        if (enemyBullet)
        {
            Debug.Log("Player hit by EnemyBullet"); // Log that the player was hit by an enemy bullet
            TakeDamage(enemyBullet.damage); // Reduce the player's lives by the bullet's damage
            Destroy(collision.gameObject); // Destroy the enemy bullet
        }

        // Controleer of de speler een vijand raakt
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("Player hit by Enemy"); // Log that the player was hit by an enemy
            TakeDamage(1); // Reduce the player's lives by 1
            Destroy(collision.gameObject); // Destroy the enemy
        }

        // Controleer of de speler een boss projectile raakt
        BossProjectile bossProjectile = collision.GetComponent<BossProjectile>();
        if (bossProjectile)
        {
            //Debug.Log("Player hit by BossProjectile"); // Log that the player was hit by a boss projectile
            TakeDamage((int)bossProjectile.damage); // Reduce the player's lives by the projectile's damage
            Freeze(bossProjectile.freezeDuration); // Apply the freeze effect to the player
            Destroy(collision.gameObject); // Destroy the boss projectile
        }
    }

    // Toegevoegd voor hit overlay
    private void ShowHitOverlay()
    {
        if (hitOverlay != null)
        {
            hitOverlay.enabled = true; // Enable the hit overlay
            overlayTimer = overlayDuration; // Reset the overlay timer
        }
    }
}
