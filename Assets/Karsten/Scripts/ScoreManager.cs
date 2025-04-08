using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public int score = 0;

    [SerializeField] private string bossSceneName = "BossScene"; // Name of the boss scene
    [SerializeField] private float bossSceneDelay = 2.0f; // Delay before loading the boss scene

    private bool isTransitioningToBoss = false; // Prevent multiple transitions

    void Awake()
    {
        // Ensure there is only one instance of the ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();

        // Check if the score has reached or exceeded 400 and prevent multiple transitions
        if (score >= 700 && !isTransitioningToBoss)
        {
            Debug.Log("Score reached 400. Starting cooldown to load boss scene."); // Log for debugging
            StartCoroutine(LoadBossSceneWithDelay());
        }
    }

    IEnumerator LoadBossSceneWithDelay()
    {
        isTransitioningToBoss = true; // Prevent multiple transitions
        yield return new WaitForSeconds(bossSceneDelay); // Wait for the specified delay
        Debug.Log("Loading boss scene now."); // Log for debugging
        SceneManager.LoadScene(bossSceneName); // Load the boss scene
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
