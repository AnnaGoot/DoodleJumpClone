using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    float horizontal;
    public Rigidbody2D DoodleRB;

    public float normalJump = 10f;
    public float boostJump = 20f;
    private float jumpForce;

    public TMP_Text scoreText;
    private float topScore = 0.0f;
    private float currentJumpStartY;

    public GameOverUIController gameOverUIController;

    private float screenLeftLimit;
    private float screenRightLimit;

    private float despawnYThreshold = -10f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        jumpForce = normalJump;

        DoodleRB = GetComponent<Rigidbody2D>();

        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        screenLeftLimit = -screenBounds.x;
        screenRightLimit = screenBounds.x;
        //screenLeftLimit = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        //screenRightLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        currentJumpStartY = transform.position.y;
    }

    void FixedUpdate()
    {
        HandleMovement();
        CheckScreenWrap();

        if (DoodleRB.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
            scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
        }
    }

    private void HandleMovement()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            horizontal = Input.acceleration.x;
        }

        if (horizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (horizontal > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        DoodleRB.velocity = new Vector2(Input.acceleration.x * 10f, DoodleRB.velocity.y);
    }


    void CheckScreenWrap()
    {
        Vector3 position = transform.position;
        if (position.x < screenLeftLimit)
        {
            position.x = screenRightLimit;
        }
        else if (position.x > screenRightLimit)
        {
            position.x = screenLeftLimit;
        }
        transform.position = position;

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        currentJumpStartY = transform.position.y;

        if (collision.gameObject.name == "DeadZone")
        {
            HandleGameOver();
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "DeadZone")
        {
            HandleGameOver();
        }
    }

    public void HandleGameOver()
    {
        if (gameOverUIController != null)
        {
            gameOverUIController.ShowGameOverScreen();
        }
        else
        {
            Debug.LogError("GameOverUIController script not found!");
        }
        Destroy(gameObject);
    }
}
