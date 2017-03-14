using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    private int score = 0;
    public Text scoreText;
    public Text gameoverText;
    public Text finalScoreText;
    public Text startText;
    public Text PauseText;

    private bool hasStarted = false;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        if (!hasStarted && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    void OnDestroy()
    {
        EventHandler.GameoverEvent -= GameOver;
        EventHandler.ResetEvent -= Initialize;
        EventHandler.ScoreEvent -= Score;
        EventHandler.PauseEvent -= Pause;
    }

    public void Initialize()
    {
        scoreText.text = score.ToString();
        gameoverText.enabled = false;
        finalScoreText.text = "Final Score: ";
        finalScoreText.enabled = false;
        startText.enabled = true;
        hasStarted = false;
        PauseText.enabled = false;
        Time.timeScale = 0;

        EventHandler.GameoverEvent += GameOver;
        EventHandler.ResetEvent += Initialize;
        EventHandler.ScoreEvent += Score;
        EventHandler.PauseEvent += Pause;
    }

    public void GameOver()
    {
        gameoverText.enabled = true;
        finalScoreText.text += score.ToString();
        finalScoreText.enabled = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startText.enabled = false;
        hasStarted = true;
    }
	
    public void Score(int incr)
    {
        score += incr;
        scoreText.text = score.ToString();
    }

    public void Pause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            PauseText.enabled = true;
        } else
        {
            Time.timeScale = 1;
            PauseText.enabled = false;
        }
    }
}
