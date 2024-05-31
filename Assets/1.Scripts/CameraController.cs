using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerPosition;


    void Update()
    {
        if (playerPosition != null)
        {
            if (playerPosition.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, playerPosition.position.y, transform.position.z);
            }
        }
       
    }
}
