using UnityEngine;
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
}
