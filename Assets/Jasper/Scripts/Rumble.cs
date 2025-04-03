using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Rumble : MonoBehaviour
{
    private Gamepad pad;

    private Coroutine stopRumble;

    public int CurentPriority = 0;

    private float ThisDuration = 0f;
    private float ThisDurationTimer = 0f;


    private void Update()
    {
        ThisDurationTimer += Time.deltaTime;
        if (ThisDuration <= ThisDurationTimer)
        {
            pad.SetMotorSpeeds(0f, 0f);
            CurentPriority = 0;
        }

    }

    public void StartRumble(float Low, float High, float Duration, int Priority)
    {
        pad = Gamepad.current;
        if (Priority >= CurentPriority)
        {
            if (pad != null)
            {
                ThisDurationTimer = 0f;

                CurentPriority = Priority;

                pad.SetMotorSpeeds(Low, High);

                ThisDuration = Duration;
            }
        }
    }
}
