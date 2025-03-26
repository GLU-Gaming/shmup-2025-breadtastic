using UnityEngine;

public class backloop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 topRight;
    private Vector3 bottomLeft;
    void Start()
    {
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10));
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
