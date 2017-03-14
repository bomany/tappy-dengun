using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance = null;
    public float multiplier = 1f;

    public float spawnCooldown = 5f;
    private float cooldown = 0;

    private int obstacleCount = 0;
    private bool canSpawn = true;

    public int maxObstacles = 3;
    public GameObject obstacle;
    public Vector3 spawnCoords;

    private float gameSpeed = 1f;
    private bool gameOver = false;
    private float gameoverCooldown = 0F;
    public List<BackgroundScroller> backgrounds;

    void Awake()
    {
        Initialize();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnCoords, new Vector3(2, 10, 0));
    }

    void Update()
    {
        SpawnObstacle();
        if (gameOver && gameoverCooldown < Time.time && Input.GetMouseButtonDown(0))
        {
            Reset();
        }
    }

    void Initialize()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        EventHandler.GameoverEvent += GameOver;
        EventHandler.ResetEvent += Reset;
        //EventHandler.PauseEvent += Pause;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canSpawn = true;
        obstacleCount = 0;
        cooldown = 0f;
        gameOver = false;
    }

    public void GameOver()
    {
        canSpawn = false;
        gameOver = true;
        gameoverCooldown = Time.time + 1f;
    }

    public void DecrObstacleCount(int count)
    {
        obstacleCount -= count;
    }

    void SpawnObstacle()
    {
        if (canSpawn && Time.time > cooldown && obstacleCount < (maxObstacles*(int)multiplier))
        {
            Instantiate(obstacle, spawnCoords, Quaternion.identity);
            obstacleCount += 1;
            cooldown = Time.time + (spawnCooldown/(int)multiplier);
        }
    }

    public float GetGameSpeed()
    {
        return multiplier;
    }

    public void Pause(bool isPaused)
    {
        canSpawn = isPaused;
        //EventHandler.TriggerPause();
    }
}
