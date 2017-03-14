using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {
    public delegate void GameoverAction();
    public static event GameoverAction GameoverEvent;

    public delegate void ResetAction();
    public static event ResetAction ResetEvent;

    public delegate void ScoreAction(int score);
    public static event ScoreAction ScoreEvent;

    public delegate void PauseAction(bool isPaused);
    public static event PauseAction PauseEvent;
    static bool isPaused = false; 

    public static void TriggerGamerOver()
    {
        if (GameoverEvent != null)
            GameoverEvent();
    }

    public static void TriggerReset()
    {
        if (ResetEvent != null)
            ResetEvent();
    }

    public static void TriggerScore(int s)
    {
        if (ScoreEvent != null)
            ScoreEvent(s);
    }

    public static void TriggerPause()
    {
        Debug.Log("Paused");
        if (PauseEvent != null)
            isPaused = !isPaused;
            PauseEvent(isPaused);
    }

}
