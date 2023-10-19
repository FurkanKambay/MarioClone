using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] CoinCollector coinCollector;
    [SerializeField] AudioClip coinCollectSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (coinCollector)
            coinCollector.CoinCollected += OnCoinCollected;
    }

    private void OnCoinCollected(int coinsCollected)
    {
        audioSource.PlayOneShot(coinCollectSound);
    }
}
