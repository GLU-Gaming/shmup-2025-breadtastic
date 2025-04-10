using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoad : MonoBehaviour
{
    public audioManager audManager;

    void Start()
    {
        audManager = FindFirstObjectByType<audioManager>();
    }

    void Awake()
    {
       audManager = FindFirstObjectByType<audioManager>(); 
    }

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
        audManager.instance.StopAllSounds();
    }
}
