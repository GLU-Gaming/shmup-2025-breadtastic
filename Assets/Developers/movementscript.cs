using UnityEngine;

public class movementscript : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // Get the vertical input (up/down arrow keys or W/S keys)
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new position
        Vector3 newPosition = transform.position + new Vector3(0, verticalInput * speed * Time.deltaTime, 0);

        // Update the player's position
        transform.position = newPosition;
    }
}
