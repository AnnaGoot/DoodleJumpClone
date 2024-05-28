using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float disapperDelay = 5f;
    private bool playerToched = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
           playerToched = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && playerToched)
        {
            Invoke("Disappear", disapperDelay);
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
        playerToched = false;
    }
}
