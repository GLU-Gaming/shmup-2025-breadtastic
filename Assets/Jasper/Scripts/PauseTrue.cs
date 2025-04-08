using UnityEngine;

public class PauseTrue : MonoBehaviour
{
    public static bool GameIsPaused = false;

    private void Update()
    {
        Debug.Log(GameIsPaused);
    }
}
