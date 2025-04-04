using UnityEngine;

public class laser : MonoBehaviour
{
    [Header("Beam Instellingen")]
    public float maxBeamLength = 10f; // Maximum lengte van de beam
    public float beamGrowthSpeed = 20f; // Hoe snel de beam groeit
    public float damage = 1f; // De damage per seconde dat de beam hit
    public LayerMask hitLayers; // Welke layer de beam hit

    // Graag deze niet zomaar aanpassen
    private float startWidth = 1f;
    private float endWidth = 1.5f;
    private float BeamEndWidth = 2.2f;
    private float BeamEndLength = 1.9f;
    private Vector3 beamEndOffset = new Vector3 (-0.55f, -0.4f, 0f);

    [Header("Beam References")]
    public Transform beamSpawnPoint;
    public LineRenderer beamRenderer;
    public LineRenderer beamEndRenderer;
    public Material beamMaterial;
    public Material beamEndMaterial;

    [Header("Over References")]
    public GameObject ovenOpen;
    public GameObject ovenClose;

    private float currentBeamLength = 0f;
    private bool isFiring = false;
    private RaycastHit hitInfo;

    void Start()
    {
        // Initialize de line renderers
        InitializeLineRenderer();

        SetBeamActive(false);
    }

    void Update()
    {
        //
        // Ik heb hier de nieuwe inputs nodig
        //          |||||||||||
        //          vvvvvvvvvvv
        //
        if (Input.GetMouseButtonDown(1))
        {
            StartFiring();
            SwitchModel();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopFiring();
            SwitchModel();
        }

        if (isFiring)
        {
            UpdateBeam();
        }
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

    void StartFiring()
    {
        isFiring = true;
        currentBeamLength = 0f;
        SetBeamActive(true);
    }

    void StopFiring()
    {
        isFiring = false;
        SetBeamActive(false);
    }

    void SetBeamActive(bool active)
    {
        beamRenderer.enabled = active;
        beamEndRenderer.enabled = active;
    }

    void UpdateBeam()
    {
        // Groeit de beam tot maximum lengte
        currentBeamLength = Mathf.Min(currentBeamLength + beamGrowthSpeed * Time.deltaTime, maxBeamLength);

        // Berekent eind punt
        Vector3 beamEnd = beamSpawnPoint.position + beamSpawnPoint.right * currentBeamLength;

        // Hit detectie
        bool hasHit = Physics.Raycast(beamSpawnPoint.position, beamSpawnPoint.right, out hitInfo, currentBeamLength, hitLayers);

        if (hasHit)
        {
            beamEnd = hitInfo.point;

        // !!!
        // Zet hier de damage op voor de enemy
        // dus doe iets zoals dit:
        /*
        Health health = hitInfo.collider.GetComponent<healthScript>();
        if (healht != null){
        health.TakeDamage(damage * Time.deltatime); }
        */
        // of idk hoe jullie damage doen in de game
        // !!!

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
    }

    public void SwitchModel()
    {
        if (isFiring)
        {
            ovenOpen.SetActive(true);
            ovenClose.SetActive(false);
        } else 
        { 
            ovenClose.SetActive(true);
            ovenOpen.SetActive(false);
        }
    }
}
