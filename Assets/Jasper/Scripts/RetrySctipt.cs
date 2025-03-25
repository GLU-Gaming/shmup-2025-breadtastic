using UnityEngine;
using UnityEngine.SceneManagement;

public class RetrySctipt : MonoBehaviour
{
    public void retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void qiut()
    {
        Application.Quit();
    }
}
