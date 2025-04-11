using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    public float Delay = 3f;
    public string SceneName = "Start";

    public RawImage fadeImage;
    public float fadeDuration;

    public bool fadedIn;

    void Start()
    {
        fadedIn = false;
        StartCoroutine(Fade(0, 1));
        Invoke("LoadNextScene", Delay); 
    }

    private void Update()
    {
        if (fadedIn)
        {
            StartCoroutine(Fade(1, 0));
        }
    }

    IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
        fadedIn = true;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
