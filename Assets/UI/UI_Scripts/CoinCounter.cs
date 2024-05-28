using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public TMP_Text coinsText;
    private int coins = 0;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + coins;
        }
        else
        {
            Debug.LogError("Coins text component not set!");
        }
    }
}

