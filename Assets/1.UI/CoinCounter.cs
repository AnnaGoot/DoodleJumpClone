using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
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
}

