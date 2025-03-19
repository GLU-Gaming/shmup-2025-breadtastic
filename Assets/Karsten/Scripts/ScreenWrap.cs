using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private float screenTop;
    private float screenBottom;

    void Start()
    {
        // Bereken de schermgrenzen
        Camera mainCamera = Camera.main;
        screenTop = mainCamera.orthographicSize;
        screenBottom = -mainCamera.orthographicSize;
    }

    void Update()
    {
        // Controleer of de speler buiten de schermgrenzen is
        if (transform.position.y > screenTop)
        {
            // Verplaats de speler naar de onderkant van het scherm
            transform.position = new Vector3(transform.position.x, screenBottom, transform.position.z);
        }
        else if (transform.position.y < screenBottom)
        {
            // Verplaats de speler naar de bovenkant van het scherm
            transform.position = new Vector3(transform.position.x, screenTop, transform.position.z);
        }
    }
}
