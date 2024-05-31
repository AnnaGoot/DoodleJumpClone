using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float forceJump;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0 && collision.collider.CompareTag("Player"))
        {
            PlayerController.instance.DoodleRB.velocity = Vector2.up * forceJump;
        }
    }
}
