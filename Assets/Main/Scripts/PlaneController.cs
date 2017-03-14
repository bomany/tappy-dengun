using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {
    private Rigidbody2D rb;
    public float upwardsForce = 10f;
    public float rotateSpeed = 45f;

    public float clickCooldown = 0.5f;
    private float clickTime = 0f;
    private bool alive = true;

    private Transform sprite;
    public GameObject smoke;
    public Animator animation;

    public bool godMode = false;

    // Unity Events
    void Start () {
        Initialize();
    }
	
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IncrementScore(other.gameObject.tag);
    }

    void FixedUpdate()
    {
        OnClick();
    }

    // My Events
    void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0);
        smoke.SetActive(false);
        EventHandler.GameoverEvent += GameOver;
    }

    void IncrementScore(string tag)
    {
        if (tag == "Obstacle")
        {
            EventHandler.TriggerScore(1);
        }
    }

    void OnDestroy()
    {
        EventHandler.GameoverEvent -= GameOver;
    }

    /// <summary>
    /// Triggered when object hits something.
    /// </summary>
    void OnHit()
    {
        if (alive && !godMode)
        {
            EventHandler.TriggerGamerOver();
        }
    }

    public void GameOver() {
        alive = false;
        smoke.SetActive(true);
        animation.Stop();
    }

    void OnClick()
    {
        if (alive && Time.time > clickTime && Input.GetMouseButtonDown(0))
        {
            rb.velocity = transform.up * upwardsForce;
            clickTime = Time.time + clickCooldown;
        }
        Rotate();
    }

    void Rotate()
    {
        float angle = rb.velocity.normalized.y * 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        sprite.rotation = Quaternion.RotateTowards(sprite.rotation, rotation, Time.deltaTime * rotateSpeed);
    }


}
