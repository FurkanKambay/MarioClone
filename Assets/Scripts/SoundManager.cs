using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] CoinCollector coinCollector;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] AudioClip coinCollectSound;
    [SerializeField] AudioClip jumpSound;

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
