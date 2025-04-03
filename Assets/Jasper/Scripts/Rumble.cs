using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Rumble : MonoBehaviour
{
    private Gamepad pad;

    private Coroutine stopRumble;

    public void StartRumble(float Low, float High, float Duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            pad.SetMotorSpeeds(Low, High);

            stopRumble = StartCoroutine(StopRumble(Duration));
        }
    }

    private IEnumerator StopRumble(float Duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0f, 0f);
    }
}
