using System.Collections;
using UnityEngine;

public class JustJump : MonoBehaviour
{
    public Rigidbody2D DoodleRB;
    public float jumpForce = 5f;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0)
        {
            DoodleRB.velocity = new Vector2(0, jumpForce);
        }
    }
}

