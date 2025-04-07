using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Rumble : MonoBehaviour
{
    private Gamepad pad;

    private Coroutine stopRumble;

    public int CurentPriority = 0;
    public float CurentLow = 0;
    public float CurentHigh = 0;

    private float ThisDuration = 0f;
    private float ThisDurationTimer = 0f;


    private void Update()
    {
        pad = Gamepad.current;
        if (pad != null)
        {
            ThisDurationTimer += Time.deltaTime;
            if (ThisDuration <= ThisDurationTimer)
            {
                pad.SetMotorSpeeds(0f, 0f);
                CurentPriority = 0;
                CurentLow = 0;
                CurentHigh = 0;
            }

            if (PauseMenu.GameIsPaused)
            {
                pad.SetMotorSpeeds(0f, 0f);
            }
            else
            {
                pad.SetMotorSpeeds(CurentLow, CurentHigh);
            }
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

                CurentHigh = High;

                CurentLow = Low;

                pad.SetMotorSpeeds(Low, High);

                ThisDuration = Duration;
            }
        }
    }
}
