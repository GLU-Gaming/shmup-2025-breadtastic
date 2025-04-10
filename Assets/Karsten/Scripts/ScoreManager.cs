using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public string bossSceneName = "BossScene"; // Name of the boss scene
    private int  scoreValue;

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

        // Check if the score reaches 600 and trigger the boss fight
        if (score >= 600 && SceneManager.GetActiveScene().name != bossSceneName)
        {
            TriggerBossFight();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void TriggerBossFight()
    {
        Debug.Log("Boss fight triggered! Loading boss scene...");

        // Load the boss scene
        if (!string.IsNullOrEmpty(bossSceneName))
        {   
            SceneManager.LoadScene(bossSceneName);
        }
        else
        {
            Debug.LogError("Boss scene name is not set in the ScoreManager.");
        }
    }
}
