using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && CoinCounter.instance != null)
        {
            CoinCounter.instance.AddCoins(1);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Player tag or CoinCounter instance not found");
        }
    }
}
