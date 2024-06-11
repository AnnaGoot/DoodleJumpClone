using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float forceJump = 10f;
    public bool isBoostPlatform = false;
    public bool isDisappearingPlatform = false;
    [SerializeField] private float disappearDelay = 0.2f;

    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    private bool playerToched = false;
    private bool playerJumpedOff = false;

    //public void Init(PlayerController controller)
    //{
    //    playerController = controller;
    //}

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not in the scene");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0 && collision.collider.CompareTag("Player"))
        {
            if (isBoostPlatform)
            {
                playerController.DoodleRB.velocity = Vector2.up * playerController.boostJump;
            }
            else
            {
                playerController.DoodleRB.velocity = Vector2.up * forceJump;
            }

            if (isDisappearingPlatform)
            {
                playerToched = true;
                playerJumpedOff = true;
                StartCoroutine(DisappearAfterJump());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && playerToched)
        {
            playerJumpedOff = true;
            if (isDisappearingPlatform)
            {
                StartCoroutine(DisappearAfterJump());
            }
        }
    }

    private IEnumerator DisappearAfterJump()
    {
        yield return new WaitForSeconds(disappearDelay);
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        Destroy(gameObject, 0.2f);
    }
}
