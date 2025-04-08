using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDead : MonoBehaviour
{
    private bool dead = false; // Boolean to track if the player is dead
    private float timerDead = 0; // Timer for the death delay
    [SerializeField] private float timerDeadEnd = 2; // Time to wait before loading the Retry scene
    [SerializeField] private int retrySceneIndex = 1; // Index of the Retry scene in the build settings

    public void Dead(bool Isdead)
    {
        dead = Isdead; // Set the dead state
        Debug.Log("Player is dead. Starting death timer."); // Log that the player is dead
    }

    private void Update()
    {
        Debug.Log("OnDead Update method is being called."); // Log that the Update method is being called

        if (dead)
        {
            Debug.Log("Dead is true. Incrementing timer."); // Log that the dead state is true
            timerDead += Time.deltaTime; // Increment the death timer
            if (timerDead > timerDeadEnd)
            {
                Debug.Log($"Attempting to load scene at index {retrySceneIndex}."); // Log the scene index being loaded
                try
                {
                    SceneManager.LoadScene(retrySceneIndex); // Load the Retry scene
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Failed to load scene at index {retrySceneIndex}: {e.Message}"); // Log any errors
                }
            }
        }
    }
}
