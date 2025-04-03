using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Rumble : MonoBehaviour
{
    private Gamepad pad;

    private Coroutine stopRumble;

    public int CurentPriority = 0;

    public void StartRumble(float Low, float High, float Duration, int Priority)
    {
        pad = Gamepad.current;
        if (Priority >= CurentPriority)
        {
            if (pad != null)
            {            
                CurentPriority = Priority;

                pad.SetMotorSpeeds(Low, High);

                stopRumble = StartCoroutine(StopRumble(Duration));
            }
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
        CurentPriority = 0;
    }
}
