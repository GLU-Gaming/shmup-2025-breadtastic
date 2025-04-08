using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    

    [SerializeField] GameObject pauseMenuUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseTrue.GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PauseTrue.GameIsPaused = true;
    }

    public void OnPause(InputValue value)
    {
        if (PauseTrue.GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
