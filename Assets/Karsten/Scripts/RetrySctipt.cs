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
        Debug.Log("B");
        quit();
    }
    
    public void OnAButton(InputValue value)
    {
        Debug.Log("A");
        retry();
    }
}
