using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIMove : OpenControls
{
    private Vector2 CurentMove;

    [SerializeField] private float TimeMove;
    private float TimerMove;

    private void Start()
    {
        TimerMove = TimeMove;
    }

    private void Update()
    {
        if(CurentMove.y == 4)
        {
            state = OnButton.On1Button;
            CurentMove.y = 0;
        }

        if (CurentMove.y == 0)
        {
            state = OnButton.On1Button;
        }
        
        if (CurentMove.y == 1)
        {
            state = OnButton.On2Button;
        }

        if (CurentMove.y == 2)
        {
            state = OnButton.On3Button;
        }

        if (CurentMove.y == -1)
        {
            state = OnButton.On3Button;
            CurentMove.y = 2;
        }

        TimerMove += Time.deltaTime;

        if (state == OnButton.On1Button)
        {
            ImageArrow.transform.position = new Vector2(ButtonList[0].transform.position.x + offset.x, ButtonList[0].transform.position.y + offset.y);

            if (Button)
            {
                retry();
            }
        }

        if (state == OnButton.On2Button)
        {
            ImageArrow.transform.position = new Vector2(ButtonList[1].transform.position.x + offset.x, ButtonList[1].transform.position.y + offset.y);

            if (Button)
            {
                if (active)
                {
                    ControlsUnActive();
                }
                else
                {
                    ControlsActive();
                }
            }
        }

        if (state == OnButton.On3Button)
        {
            ImageArrow.transform.position = new Vector2(ButtonList[2].transform.position.x + offset.x, ButtonList[2].transform.position.y + offset.y);

            if (Button)
            {
                quit();
            }
        }

        Move();
        Button = false;
    }

    private void Move()
    {
        if (move.y >= 0.1 && TimerMove >= TimeMove)
        {
            CurentMove.y -= 1;
            TimerMove = 0;
        }

        if (move.y <= -0.1 && TimerMove >= TimeMove)
        {
            CurentMove.y += 1;
            TimerMove = 0;
        }
    }
}
