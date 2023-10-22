using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private CoinCollector coinCollector;
    [SerializeField] private TMP_Text coinText;

    private void Awake()
    {
        Assert.IsNotNull(coinCollector, $"{nameof(coinCollector)} on {name}");
        Assert.IsNotNull(coinText, $"{nameof(coinText)} on {name}");

        coinCollector.CoinCollected += OnCoinCollected;
    }

    private void OnCoinCollected(int coinsCollected)
    {
        coinText.text = coinsCollected.ToString();
    }
}
