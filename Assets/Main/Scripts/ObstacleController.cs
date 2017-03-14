using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {
    public float moveSpeed = 5f;
    private bool canMove = true;

    public float padding;
    private float xmin;
    private float xmax;

    public float maxHeight = 4.2f;
    public float minHeight = 0.5f;
    public float passageHeight = 1f;

    public Sprite[] rockSprites;

    void Start () {
        Initialize();
    } 

	void Update () {
        Move();
    }

    void OnDestroy()
    {
        EventHandler.GameoverEvent -= GameOver;
        EventHandler.PauseEvent -= Pause;
    }

    void Initialize()
    {
        GetCameraViewportCoords();
        SetDificulty();
        EventHandler.GameoverEvent += GameOver;
        EventHandler.PauseEvent += Pause;

        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().sprite = rockSprites[Random.Range(0, rockSprites.Length)];
        }
    }

    void GetCameraViewportCoords()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;
    }

    void SetDificulty()
    {
        float bottomHeight = Random.Range(minHeight, maxHeight - minHeight - passageHeight);
        float topHeight = maxHeight - bottomHeight - passageHeight;
        transform.GetChild(0).localScale = new Vector3(2, bottomHeight, 1);
        transform.GetChild(1).localScale = new Vector3(2, topHeight, 1);
    }

    void Move()
    {
        if (canMove)
        {
            float speed = moveSpeed * GameController.instance.GetGameSpeed();
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        DestroyOnScreenExit();
    }

    public void GameOver()
    {
        //EventHandler.GameoverEvent -= GameOver;
        canMove = false;
        foreach (Transform rock in transform)
        {
            rock.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Pause(bool isPaused)
    {
        canMove = isPaused;
    }

    void DestroyOnScreenExit()
    {
        if (transform.position.x < xmin)
        {
            Destroy(gameObject);
            GameController.instance.DecrObstacleCount(1);
        }
    }
}
