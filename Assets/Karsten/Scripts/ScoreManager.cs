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

        // Heal the player by 1 HP for each kill
        if (points > 0) // Assuming points > 0 indicates a kill
        {
            Player player = FindObjectOfType<Player>();
            if (player != null && player.playerHP != null)
            {
                player.playerHP.Heal(1); // Heal the player by 1 HP
            }
        }

        // Check if the score reaches 700
        if (score >= 600)
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
