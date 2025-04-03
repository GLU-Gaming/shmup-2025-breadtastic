using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RetrySctipt : MonoBehaviour
{
    public void retry()
    {
        SceneManager.LoadScene("MainScene 2");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void OnBButton(InputValue value)
    {
        Application.Quit();
        Debug.Log("B");
    }

    public void OnAButton(InputValue value)
    {
        SceneManager.LoadScene("MainScene 2");
    }
}
