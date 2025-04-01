using UnityEngine;
using UnityEngine.InputSystem;


public class movementscript : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    private Vector2 MoveImput;
    private Rigidbody Rb;

    private Vector3 topRight;
    private Vector3 bottomLeft;
    [SerializeField] private Vector2 PlayerSize;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
         
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -10));
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -10));
    }

    private void Update()
    {
        if(transform.position.x - PlayerSize.x / 2 < topRight.x)
        {
            transform.position = new Vector3(topRight.x + PlayerSize.x / 2, transform.position.y, 0);
        }
        
        if(transform.position.y - PlayerSize.y / 2 < topRight.y)
        {
            transform.position = new Vector3(transform.position.x, topRight.y + PlayerSize.y / 2, 0);
        }
        
        if(transform.position.x + PlayerSize.x / 2 > bottomLeft.x)
        {
            transform.position = new Vector3(bottomLeft.x - PlayerSize.x / 2, transform.position.y, 0);
        }
        
        if(transform.position.y + PlayerSize.y / 2 > bottomLeft.y)
        {
            transform.position = new Vector3(transform.position.x, bottomLeft.y - PlayerSize.y / 2, 0);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move() 
    {
            Rb.AddForce(new Vector3(Speed * MoveImput.x, Speed * MoveImput.y, 0));
        
    }
   

    public void OnMove(InputValue value)
    {
        // Get the input (WASD keys)
        MoveImput = value.Get<Vector2>();
    }
}
