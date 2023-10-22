using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private CoinCollector coinCollector;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AudioClip coinCollectSound;
    [SerializeField] private AudioClip jumpSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (coinCollector) coinCollector.CoinCollected += OnCoinCollected;
        if (playerMovement) playerMovement.Jumped += OnJumped;
    }

    private void OnCoinCollected(int _) => audioSource.PlayOneShot(coinCollectSound);
    private void OnJumped() => audioSource.PlayOneShot(jumpSound);
}
