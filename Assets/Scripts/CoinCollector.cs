using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public event Action<int> CoinCollected;

    private int coinsCollected = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinsCollected++;
            CoinCollected?.Invoke(coinsCollected);
        }
    }
}
