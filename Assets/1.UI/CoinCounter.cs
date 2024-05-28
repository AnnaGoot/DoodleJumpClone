using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    //public static CoinCounter instance;
    public TMP_Text coinsText;
    private int coinCount = 0;

    private void Start()
    {
        UpdateCoinText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinText();
            Destroy(other.gameObject);
        }
    }

    private void UpdateCoinText()
    {
        coinsText.text = "Coins: " + coinCount;
    }

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public void AddCoins(int amount)
    //{
    //    coins += amount;
    //    if (coinsText != null)
    //    {
    //        coinsText.text = "Coins: " + coins;
    //    }
    //    else
    //    {
    //        Debug.LogError("Coins text component not set!");
    //    }
    //}
}

