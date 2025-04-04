using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate
    public GameObject background; // Reference to the background GameObject
    public GameObject toonbank; // Reference to the toonbank prefab
    public int numberOfPrefabs = 3; // Number of prefabs to manage
    public float prefabWidth = 5f; // Width of each prefab
    public float moveSpeed = 2f; // Speed at which the prefabs move
    public float yOffset = -2f; // Offset to move prefabs further down
    public float zOffset = 1f; // Offset to move prefabs in front of the background
    public float spacing = 2f; // Additional spacing between prefabs

    private List<GameObject> prefabs = new List<GameObject>();

    void Start()
    {
        // Initialize the prefabs
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 position = new Vector3(background.transform.position.x + i * (prefabWidth + spacing), background.transform.position.y + yOffset, background.transform.position.z + zOffset);
            Quaternion rotation = Quaternion.Euler(0, -90, 0); // Rotate 90 degrees around the Y-axis
            GameObject newPrefab = Instantiate(prefab, position, rotation);
            newPrefab.transform.localScale = toonbank.transform.localScale; // Set the scale to match the toonbank prefab
            prefabs.Add(newPrefab);
            Debug.Log($"Initialized prefab at position: {position} with rotation: {rotation.eulerAngles} and scale: {newPrefab.transform.localScale}");
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
        if (prefabs[0].transform.position.x < -prefabWidth * numberOfPrefabs)
        {
            // Reposition the leftmost prefab to the right
            GameObject leftmostPrefab = prefabs[0];
            prefabs.RemoveAt(0);
            float newXPosition = prefabs[prefabs.Count - 1].transform.position.x + prefabWidth + spacing;
            leftmostPrefab.transform.position = new Vector3(newXPosition, background.transform.position.y + yOffset, background.transform.position.z + zOffset);
            prefabs.Add(leftmostPrefab);
            Debug.Log($"Repositioned prefab to position: {leftmostPrefab.transform.position}");
        }
    }
}
