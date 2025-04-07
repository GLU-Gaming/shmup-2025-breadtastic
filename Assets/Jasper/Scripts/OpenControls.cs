using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum OnButton
{
    On1Button,
    On2Button,
    On3Button
}

public class OpenControls : MonoBehaviour
{
    public Vector2 move;

    public List<GameObject> ButtonList;

    [SerializeField] private GameObject ControlsImage;
    public GameObject ImageArrow;

    public OnButton state;

    public bool active = false;

    public Vector2 offset;

    public bool Button = false;

    public static bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;

    private void Start()
    {
        state = OnButton.On1Button;
    }

    public void retry()
    {
        SceneManager.LoadScene("MainScene 2");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void Control()
    {
        if (active)
        {
            ControlsUnActive();
        }
        else
        {
            ControlsActive();
        }
    }

    public void ControlsActive ()
    {
        ControlsImage.SetActive(true);
        ImageArrow.SetActive(false);
        active = true;
    }
    
    public void ControlsUnActive ()
    {
        ControlsImage.SetActive(false);
        ImageArrow.SetActive(true);
        active = false;
    }

    public void OnAButton(InputValue Value)
    {
        if (GameIsPaused)
        {
            Button = true;
        }
    }

    public void OnMoveUI(InputValue Value)
    {
        move = Value.Get<Vector2>();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OnPause(InputValue value)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void OnLoadStart()
    {
        SceneManager.LoadScene("Start");
    }
}
