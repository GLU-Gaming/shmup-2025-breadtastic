using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class BackLoop : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] Transform target;
    [SerializeField] float resetPositionX = 10.0f; // Position to reset the item to when it goes out of view
    [SerializeField] float outOfViewPositionX = -10.0f; // Position to check if the item is out of view

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(-speed, 0, 0);
    }

    void Update()
    {
        if (transform.position.x < outOfViewPositionX)
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = resetPositionX;
        transform.position = newPosition;
    }
}
