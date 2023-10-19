using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] CoinCollector coinCollector;
    [SerializeField] TMP_Text coinText;

    private void Awake()
    {
        if (coinCollector == null)
        {
            Debug.LogWarning("CoinCollector not set on " + name);
            return;
        }

        if (coinText == null)
        {
            Debug.LogWarning("CoinText not set on " + name);
            return;
        }

        coinCollector.CoinCollected += OnCoinCollected;
    }

    private void OnCoinCollected(int coinsCollected)
    {
        coinText.text = coinsCollected.ToString();
    }
}
