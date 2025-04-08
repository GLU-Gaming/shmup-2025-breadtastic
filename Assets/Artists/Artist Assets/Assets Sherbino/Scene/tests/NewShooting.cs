using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NewShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float fireRate = 0.01f; // De tijd tussen het afvuren van kogels voor de laser
    public float bulletCooldown = 0.5f; // De cooldown tijd voor het schieten met de linkermuisknop
    public float bulletSpeed = 100.0f; // De snelheid van de kogel
    public GameObject bulletPrefab; // De prefab van de kogel
    public Transform bulletSpawnpoint; // Het punt waar de kogel wordt gespawned

    [Header("Laser Settings")]
    public float damage = 1f; // De damage per seconde dat de beam hit
    public float laserCooldown = 10.0f; // De cooldown tijd voor de laser
    public float laserDuration = 2.0f; // De tijd dat de laser kan worden gebruikt
    public float maxBeamLength = 20f; // Maximum lengte van de beam
    public float beamGrowthSpeed = 20f; // Hoe snel de beam groeit
    public LayerMask hitLayers; // Welke layer de beam hit
    public Image laserCooldownImage; // UI-element voor de laser cooldown

    [Header("Beam References")]
    public Transform beamSpawnPoint; // Waar de beam spawnt
    public LineRenderer beamRenderer; // Voor de beam te renderen
    public LineRenderer beamEndRenderer; // Voor het einde van de beam te renderen
    public Material beamMaterial; // Hoe de beam eruit ziet
    public Material beamEndMaterial; // Hoe het einde van de beam eruit ziet

    // Graag deze niet zomaar aanpassen
    private float startWidth = 1f;
    private float endWidth = 1.5f;
    private float BeamEndWidth = 2.2f;
    private float BeamEndLength = 1.9f;
    private Vector3 beamEndOffset = new Vector3(-0.55f, -0.4f, 0f);

    [Header("Over References")]
    public GameObject ovenOpen; // Open model van de oven
    public GameObject ovenClose; // Gesloten model van de oven

    private float currentBeamLength = 0f;
    private RaycastHit hitInfo;

    private bool isFiring = false;
    private bool isShooting = false;
    private float nextFireTime = 0f;
    private float nextLaserTime = 0f;

    private float buttonMain = 0;
    private float buttonSecondary = 0;

    public Rumble rumble;

    private void Start()
    {
        rumble = FindFirstObjectByType<Rumble>();

        // Initialize de line renderers
        InitializeLineRenderer();

        SetBeamActive(false);
    }

    void Update()
    {
        // Controleer of de linkermuisknop is ingedrukt en de cooldown is verstreken
        if (buttonMain != 0 && Time.time >= nextFireTime)
        {
            SwitchModel();
            ShootBullet();
            nextFireTime = Time.time + bulletCooldown;
        } else if (buttonMain == 0)
        {
            isShooting = false;
            SwitchModel();
        }

        // Controleer of de rechtermuisknop is ingedrukt en de laser cooldown is verstreken
        if (buttonSecondary != 0 && Time.time >= nextLaserTime)

            // Controleer of de rechtermuisknop is ingedrukt
            if (buttonSecondary != 0)
            {
                if (!isFiring)
                {
                    StartCoroutine(FireLaser());
                }
            }

        if (buttonSecondary == 0)
        {
            isFiring = false;
        }
    }

    IEnumerator FireLaser()
    {
        isFiring = true;
        nextLaserTime = Time.time + laserCooldown;
        CoolDown coolDown = FindFirstObjectByType<CoolDown>();
        coolDown.LaserCooldown = 0;
        SwitchModel();
        SetBeamActive(true);

        float laserEndTime = Time.time + laserDuration;
        currentBeamLength = 0f;

        while (isFiring && Time.time < laserEndTime)
        {
            UpdateBeam();
            yield return new WaitForSeconds(fireRate);
        }

        isFiring = false;
        SetBeamActive(false);
        SwitchModel();
    }

    // Als dit niet word gebruikt kan dit niet weg?
    void Shoot()
    {
        // Maak een nieuwe kogel aan op de positie van de BulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, Quaternion.Euler(0, 0, -90));

        // Voeg snelheid toe aan de kogel
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnpoint.right * bulletSpeed;
        }

        // Vernietig de kogel na 1.7 seconden
        Destroy(bullet, 1.7f);
    }

    void ShootBullet()
    {
        isShooting = true;

        // Maak een nieuwe kogel aan op de positie van de BulletSpawnpoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, Quaternion.Euler(0, 0, -90));
        if (rumble)
        {
            rumble.StartRumble(0.3f, 0f, 0.2f, 1);
        }

        // Voeg snelheid toe aan de kogel
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnpoint.right * bulletSpeed;
        }
        SwitchModel();

        // Vernietig de kogel na 1.7 seconden
        Destroy(bullet, 2.5f);
    }

    public void OnMianAttack(InputValue value)
    {
        buttonMain = value.Get<float>();
    }

    public void OnAttack(InputValue Value)
    {
        buttonSecondary = Value.Get<float>();
    }

    void InitializeLineRenderer()
    {
        // Beam setup
        beamRenderer.positionCount = 2;
        beamRenderer.material = beamMaterial;
        beamRenderer.startWidth = startWidth;
        beamRenderer.endWidth = endWidth;

        // Beam end setup
        beamEndRenderer.positionCount = 2;
        beamEndRenderer.material = beamEndMaterial;
        beamEndRenderer.startWidth = BeamEndWidth;
        beamEndRenderer.endWidth = BeamEndWidth;
    }

    void SetBeamActive(bool active)
    {
        beamRenderer.enabled = active;
        beamEndRenderer.enabled = active;
    }

    void UpdateBeam()
    {
        // Heb de rumble gewoon hier gezet want zag hem op de vorige laser, als hij niet hier moet kan hij gewoon weg
        /*
        if (rumble)
        {
            rumble.StartRumble(0.3f, 0.1f, 0.2f, 1);
        }
        */

        // Groeit de beam tot maximum lengte
        currentBeamLength = Mathf.Min(currentBeamLength + beamGrowthSpeed * Time.deltaTime, maxBeamLength);

        // Berekent eind punt
        Vector3 beamEnd = beamSpawnPoint.position + beamSpawnPoint.right * currentBeamLength;

        // Hit detectie
        bool hasHit = Physics.Raycast(beamSpawnPoint.position, beamSpawnPoint.right, out hitInfo, currentBeamLength, hitLayers);

        if (hasHit)
        {
            beamEnd = hitInfo.point;

            // Enemy Damage
            Enemy health = hitInfo.collider.GetComponent<Enemy>();
            if (health != null)
            {
            health.TakeDamage(damage * Time.deltaTime);
            }
        }

        // Update beam pos
        beamRenderer.SetPosition(0, beamSpawnPoint.position);
        beamRenderer.SetPosition(1, beamEnd);

        // Update beam end
        UpdateBeamEndEffect(beamEnd);
    }

    void UpdateBeamEndEffect(Vector3 endPosition)
    {
        Vector3 offsetPosition = endPosition;

        offsetPosition += beamSpawnPoint.right * beamEndOffset.x;
        offsetPosition += beamSpawnPoint.up * beamEndOffset.y;
        offsetPosition += beamSpawnPoint.forward * beamEndOffset.z;

        // Calculate end effect positions (using length as before)
        Vector3 endEffectDirection = beamSpawnPoint.up; // Perpendicular direction

        beamEndRenderer.SetPosition(0, offsetPosition - (endEffectDirection * BeamEndLength));
        beamEndRenderer.SetPosition(1, offsetPosition + (endEffectDirection * BeamEndLength));

        if (rumble)
        {
            rumble.StartRumble(0.3f, 0.5f, 0.2f, 1);
        }
    }

    public void SwitchModel()
    {
        if (isFiring || isShooting)
        {
            ovenOpen.SetActive(true);
            ovenClose.SetActive(false);
        }
        else
        {
            ovenClose.SetActive(true);
            ovenOpen.SetActive(false);
        }
    }
}