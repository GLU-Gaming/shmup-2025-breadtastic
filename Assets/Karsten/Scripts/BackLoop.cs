using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class BackLoop : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    Rigidbody rb;
    Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce( new Vector3(-speed, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < target.position.x)
        {
           
        }
    }
}
