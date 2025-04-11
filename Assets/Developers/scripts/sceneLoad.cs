using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoad : MonoBehaviour
{

 
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Checkt elke keer dat scene geladen word welke scene het is
    {
        audioManager.instance.StopAllSounds();
    }
}
