using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMovePause : OpenControls
{
    public Vector2 CurentMove;

    [SerializeField] private float TimeMove;
    private float TimerMove;
    
    [SerializeField] private float TimeMoveUpAndDown;
    private float TimerMoveUpAndDown;

    [SerializeField] private TextMeshProUGUI ScoreText;
    private float ScoreNum = 0;
    private ScoreManager scoreManager;

    private void Start()
    {
        TimerMove = TimeMove;
        TimerMoveUpAndDown = TimeMoveUpAndDown;
        Resume();

        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    private void Update()
    {
        if (scoreManager)
        {
            ScoreNum = scoreManager.score;
        }

        ScoreText.text = "" + ScoreNum;

        TimerMove += Time.unscaledDeltaTime;

        if (CurentMove.x <= -1 && CurentMove.y == 0)
        {
            state = OnButton.On2Button;
            CurentMove.x = 1;
        }

        if (CurentMove.x == 0 && CurentMove.y == 0)
        {
            state = OnButton.On1Button;
        }

        if (CurentMove.x == 1 && CurentMove.y == 0)
        {
            state = OnButton.On2Button;
        }
        
        if (CurentMove.x >= 2 && CurentMove.y == 0)
        {
            state = OnButton.On1Button;
            CurentMove.x = 0;
        }

        if (CurentMove.y >= 2)
        {
            CurentMove.y = 0;
            TimerMoveUpAndDown = 0;
        }

        if (CurentMove.y == 1)
        {
            state = OnButton.On3Button;
            TimerMoveUpAndDown += Time.unscaledDeltaTime;
        }

        if (CurentMove.y <= -1)
        {
            CurentMove.y = 1;
        }

        if (move.x >= 0.1f && CurentMove.y == 1 && TimerMoveUpAndDown >= TimeMoveUpAndDown)
        {
            CurentMove = new Vector2(0, 0);
            TimerMoveUpAndDown = 0;
        }
        
        if (move.x <= -0.1f && CurentMove.y == 1 && TimerMoveUpAndDown >= TimeMoveUpAndDown)
        {
            CurentMove = new Vector2(1, 0);
            TimerMoveUpAndDown = 0;
        }

        if (state == OnButton.On1Button)
        {
            ImageArrow.transform.position = new Vector2(ButtonList[0].transform.position.x + offset.x, ButtonList[0].transform.position.y + offset.y);

            if (Button)
            {
                Resume();
                Button = false;
            }
        }

        if (state == OnButton.On2Button)
        {
            ImageArrow.transform.position = new Vector2(ButtonList[1].transform.position.x + offset.x, ButtonList[1].transform.position.y + offset.y);

            if (Button)
            {
                Time.timeScale = 1f;
                OnLoadStart();
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

        if (GameIsPaused)
        {
            Move();
        }
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

        if(move.x >= 0.1 && TimerMove >= TimeMove)
        {
            CurentMove.x -= 1;
            TimerMove = 0;
        }
        
        if(move.x <= -0.1 && TimerMove >= TimeMove)
        {
            CurentMove.x += 1;
            TimerMove = 0;
        }
    }
}
