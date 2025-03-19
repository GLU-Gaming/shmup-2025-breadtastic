using UnityEngine;

public class movementscript : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // Get the vertical input (up/down arrow keys or W/S keys)
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new position
        

        // Update the player's position
        
    }

    public void OnMove(Vector2 Move)
    {
        Vector3 Direction = new Vector3(0, Move.y, 0);
        Vector3 newPosition = transform.position + new Vector3(0, Direction.y * speed * Time.deltaTime, 0);

        transform.position = newPosition;
    }
}
