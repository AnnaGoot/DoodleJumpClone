using System.Collections;
using System.Collections.Generic;
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

    public GameOverUIController gameOverUIController;

    private float screenLeftLimit;
    private float screenRightLimit;

    private void Start()
    {
        jumpForce = normalJump;

        screenLeftLimit = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRightLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

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

    void FixedUpdate()
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

        CheckScreenWrap();
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "DeadZone")
        {
            if (gameOverUIController != null)
            {
                gameOverUIController.ShowGameOverScreen();
            }
            else
            {
                Debug.LogError("GameOverUIController script not found!");
            }
            this.enabled = false;
        }
        
        
    }
}
