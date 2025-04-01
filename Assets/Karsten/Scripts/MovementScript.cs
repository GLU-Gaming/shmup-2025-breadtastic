using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector2 moveInput;
    private Rigidbody rb;

    private Vector3 topRight;
    private Vector3 bottomLeft;
    [SerializeField] private Vector2 playerSize;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
    }

    private void Update()
    {
        if (transform.position.x - playerSize.x / 2 < bottomLeft.x)
        {
            transform.position = new Vector3(bottomLeft.x + playerSize.x / 2, transform.position.y, 0);
        }

        if (transform.position.y - playerSize.y / 2 < bottomLeft.y)
        {
            transform.position = new Vector3(transform.position.x, bottomLeft.y + playerSize.y / 2, 0);
        }

        if (transform.position.x + playerSize.x / 2 > topRight.x)
        {
            transform.position = new Vector3(topRight.x - playerSize.x / 2, transform.position.y, 0);
        }

        if (transform.position.y + playerSize.y / 2 > topRight.y)
        {
            transform.position = new Vector3(transform.position.x, topRight.y - playerSize.y / 2, 0);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.AddForce(new Vector3(speed * moveInput.x, speed * moveInput.y, 0));
    }

    public void OnMove(InputValue value)
    {
        // Get the input (WASD keys)
        moveInput = value.Get<Vector2>();
    }
}
