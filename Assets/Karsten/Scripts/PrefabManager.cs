using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate
    public GameObject background; // Reference to the background GameObject
    public int numberOfPrefabs = 3; // Number of prefabs to manage
    public float prefabWidth = 5f; // Width of each prefab
    public float moveSpeed = 2f; // Speed at which the prefabs move
    public float yOffset = -2f; // Offset to move prefabs further down

    private List<GameObject> prefabs = new List<GameObject>();

    void Start()
    {
        // Initialize the prefabs
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 position = new Vector3(background.transform.position.x + i * prefabWidth, background.transform.position.y + yOffset, background.transform.position.z);
            Quaternion rotation = Quaternion.Euler(0, -90, 0); // Rotate 90 degrees around the Y-axis
            GameObject newPrefab = Instantiate(prefab, position, rotation);
            prefabs.Add(newPrefab);
            Debug.Log($"Initialized prefab at position: {position} with rotation: {rotation.eulerAngles}");
        }
    }

    void Update()
    {
        // Move the prefabs to the left
        foreach (GameObject prefab in prefabs)
        {
            prefab.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }

        // Check if the leftmost prefab is out of view
        if (prefabs[0].transform.position.x < -prefabWidth * numberOfPrefabs / 2)
        {
            // Remove the leftmost prefab
            GameObject leftmostPrefab = prefabs[0];
            prefabs.RemoveAt(0);
            Destroy(leftmostPrefab);
            Debug.Log("Removed leftmost prefab");

            // Instantiate a new prefab on the right
            Vector3 newPosition = new Vector3(prefabs[prefabs.Count - 1].transform.position.x + prefabWidth, background.transform.position.y + yOffset, background.transform.position.z);
            Quaternion rotation = Quaternion.Euler(0, -90, 0); // Rotate 90 degrees around the Y-axis
            GameObject newPrefab = Instantiate(prefab, newPosition, rotation);
            prefabs.Add(newPrefab);
            Debug.Log($"Instantiated new prefab at position: {newPosition} with rotation: {rotation.eulerAngles}");
        }
    }
}
