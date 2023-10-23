using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip coinCollectSound;
    [SerializeField] private AudioClip bulletFireSound;

    private Health playerHealth;
    private PlayerMovement playerMovement;
    private CoinCollector coinCollector;
    private PlayerAttack playerAttack;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        var player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        playerMovement = player.GetComponent<PlayerMovement>();
        coinCollector = player.GetComponent<CoinCollector>();
        playerAttack = player.GetComponentInChildren<PlayerAttack>();

        if (playerHealth)
        {
            if (damageSound) playerHealth.DamageTaken += _ => audioSource.PlayOneShot(damageSound);
            if (deathSound) playerHealth.Died += () => audioSource.PlayOneShot(deathSound);
        }

        if (coinCollector && coinCollectSound)
            coinCollector.CoinCollected += _ => audioSource.PlayOneShot(coinCollectSound);

        if (playerMovement && jumpSound)
            playerMovement.Jumped += () => audioSource.PlayOneShot(jumpSound);

        if (playerAttack && bulletFireSound)
            playerAttack.BulletFired += () => audioSource.PlayOneShot(bulletFireSound);
    }
}
