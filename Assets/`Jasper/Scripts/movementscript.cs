using UnityEngine;
using UnityEngine.InputSystem;


public class movementscript : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 MoveImput;

    void Update()
    {        
        Move();
    }

    private void Move()
    {


        // Calculate the new position
        Vector3 newPosition = transform.position + new Vector3(MoveImput.x * speed * Time.deltaTime, MoveImput.y * speed * Time.deltaTime, 0);

        // Update the player's position
        transform.position = newPosition;
    }

    public void OnMove(InputValue value)
    {
        // Get the input (WASD keys)
        MoveImput = value.Get<Vector2>();
    }
}
