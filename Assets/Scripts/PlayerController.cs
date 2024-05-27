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

    private void Start()
    {
        jumpForce = normalJump;
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

    void Update()
    {
        
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
        
        
        
        //foreach (ContactPoint2D point in collision.contacts)
        //{
        //    if (point.normal.y >= 0.5f)
        //    {
        //        if (collision.collider.CompareTag("BoostPlatform"))
        //        {
        //            jumpForce = boostJump;
        //        }
        //        else if (collision.collider.CompareTag("Platform"))
        //        {
        //            jumpForce = normalJump;
        //        }
        //        DoodleRB.velocity = Vector2.up * jumpForce;
        //        break;
        //    }
        //}



        //if (collision.collider.name == "DeadZone")
        //{
        //    if (gameOverUIController != null)
        //    {
        //        gameOverUIController.ShowGameOverScreen();
        //    }
        //    else
        //    {
        //        Debug.LogError("GameOverUIController script not found!");
        //    }
        //}
    }
}
