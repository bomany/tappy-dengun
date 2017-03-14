using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    public float speed = 5;
    public float startOffset = 0;
    private Renderer rend;
    public bool canMove = true;

    void Start()
    {
        Initialize();
    }

    void Destroy()
    {
        EventHandler.GameoverEvent -= GameOver;
        EventHandler.PauseEvent -= Pause;
    }

    void Update () {
        Move();
	}

    void Initialize()
    {
        rend = GetComponent<Renderer>();
        EventHandler.GameoverEvent += GameOver;
        EventHandler.PauseEvent += Pause;
    }

    void Move()
    {
        if (canMove)
        {
            float Multpliedspeed = speed * GameController.instance.GetGameSpeed();
            float offset = Time.time * Multpliedspeed + startOffset;
            rend.material.mainTextureOffset = new Vector2(offset / transform.localScale.x, 0);
        }
    }

    void GameOver()
    {
        //EventHandler.GameoverEvent -= GameOver;
        canMove = false;
    }

    public void Pause(bool isPaused)
    {
        canMove = isPaused;
    }

    void SetSpeed(float s)
    {
        //multiplier = s;
    }
}
