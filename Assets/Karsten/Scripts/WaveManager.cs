using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public Text waveText;
    public  int waveCounter = 0;

    void Awake()
    {
        // Ensure there is only one instance of the WaveManager
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
        UpdateWaveText();
    }

    public void IncrementWave()
    {
        waveCounter++;
        UpdateWaveText();
    }

    void UpdateWaveText()
    {
        waveText.text = "Wave: " + waveCounter;
    }
}
