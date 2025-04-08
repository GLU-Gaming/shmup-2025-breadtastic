using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate
    public GameObject background; // Reference to the background GameObject
    public GameObject toonbank; // Reference to the toonbank prefab
    public List<GameObject> additionalPrefabs; // List of additional prefabs to move with the background
    public int numberOfPrefabs = 3; // Number of prefabs to manage
    public float prefabWidth = 5f; // Width of each prefab
    public float moveSpeed = 2f; // Speed at which the prefabs move
    public float yOffset = -2f; // Offset to move prefabs further down
    public float zOffset = 1f; // Offset to move prefabs in front of the background
    public float spacing = 2f; // Additional spacing between prefabs
    public float leftOffset = -10f; // Additional offset to move prefabs further to the left

    private List<GameObject> prefabs = new List<GameObject>();

    void Start()
    {
        // Calculate the starting X position to center the prefabs and apply the left offset
        float startX = -((numberOfPrefabs - 1) * (prefabWidth + spacing)) / 2 + leftOffset;

        // Initialize the prefabs
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 position = new Vector3(startX + i * (prefabWidth + spacing), background.transform.position.y + yOffset, background.transform.position.z + zOffset);
            Quaternion rotation = Quaternion.Euler(0, -90, 0); // Rotate 90 degrees around the Y-axis
            GameObject newPrefab = Instantiate(prefab, position, rotation);
            newPrefab.transform.localScale = toonbank.transform.localScale; // Set the scale to match the toonbank prefab
            prefabs.Add(newPrefab);
            Debug.Log($"Initialized prefab at position: {position} with rotation: {rotation.eulerAngles} and scale: {newPrefab.transform.localScale}");
        }

        // Initialize additional prefabs
        foreach (GameObject additionalPrefab in additionalPrefabs)
        {
            Vector3 position = new Vector3(startX, background.transform.position.y + yOffset, background.transform.position.z + zOffset);
            Quaternion rotation = Quaternion.Euler(0, -90, 0); // Rotate 90 degrees around the Y-axis
            GameObject newAdditionalPrefab = Instantiate(additionalPrefab, position, rotation);
            newAdditionalPrefab.transform.localScale = toonbank.transform.localScale; // Set the scale to match the toonbank prefab
            prefabs.Add(newAdditionalPrefab);
            Debug.Log($"Initialized additional prefab at position: {position} with rotation: {rotation.eulerAngles} and scale: {newAdditionalPrefab.transform.localScale}");
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
        if (prefabs[0].transform.position.x < -prefabWidth * (numberOfPrefabs + 1) - 80)
        {
            // Remove the leftmost prefab
            GameObject leftmostPrefab = prefabs[0];
            prefabs.RemoveAt(0);
            Destroy(leftmostPrefab);
            Debug.Log("Removed leftmost prefab");

            // Instantiate a new prefab on the right
            Vector3 newPosition = new Vector3(prefabs[prefabs.Count - 1].transform.position.x + prefabWidth + spacing, background.transform.position.y + yOffset, background.transform.position.z + zOffset);
            Quaternion rotation = Quaternion.Euler(0, -90, 0); // Rotate 90 degrees around the Y-axis
            GameObject newPrefab = Instantiate(prefab, newPosition, rotation);
            newPrefab.transform.localScale = toonbank.transform.localScale; // Set the scale to match the toonbank prefab
            prefabs.Add(newPrefab);
            Debug.Log($"Instantiated new prefab at position: {newPosition} with rotation: {rotation.eulerAngles} and scale: {newPrefab.transform.localScale}");
        }
    }
}
