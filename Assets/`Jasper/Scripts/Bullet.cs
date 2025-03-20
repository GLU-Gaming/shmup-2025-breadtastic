using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    private Rigidbody Rb;
    private Rigidbody2D Rb2D;
    [SerializeField] private float Force = 1f;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Rb2D = GetComponent<Rigidbody2D>();


        if (Rb)
        {
            Rb.linearVelocity = new Vector3(Force, 0, 0);
        }

        if (Rb2D)
        {
            Rb2D.linearVelocity = new Vector2(Force, 0);
        }
    }
}
