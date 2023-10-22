using UnityEngine;
using UnityEngine.Assertions;

public class HealthUI : MonoBehaviour
{
    private Health playerHealth;
    private RectTransform healthBar;
    private Vector2 heartIconScale;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        Assert.IsNotNull(playerHealth, $"{nameof(playerHealth)} on {name}");

        healthBar = GetComponent<RectTransform>();
        heartIconScale = healthBar.rect.size;

        playerHealth.DamageTaken += _ => UpdateHealth();
        playerHealth.Respawned += UpdateHealth;
    }

    private void Start() => UpdateHealth();

    private void UpdateHealth()
    {
        float width = playerHealth.CurrentHealth * heartIconScale.x;
        healthBar.sizeDelta = new Vector2(width, healthBar.sizeDelta.y);
    }
}
