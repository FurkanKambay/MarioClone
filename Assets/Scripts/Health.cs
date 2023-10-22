using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<Vector3> DamageTaken;
    public event Action Died;
    public event Action Respawned;

    public int MaxHealth = 3;
    public int CurrentHealth;
    public float InvincibilityTime = 1f;
    public float RespawnTime = 3f;

    [SerializeField] private Vector3 respawnPosition;

    private float lastDamageTime;

    private void Awake() => CurrentHealth = MaxHealth;

    public bool InflictDamage(int damage, Vector3 sourcePosition)
    {
        if (Time.time - lastDamageTime < InvincibilityTime)
            return false;

        lastDamageTime = Time.time;

        CurrentHealth -= damage;
        DamageTaken?.Invoke(sourcePosition);

        if (CurrentHealth <= 0)
            Die();

        return true;
    }

    private void Die()
    {
        Died?.Invoke();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTime);
        CurrentHealth = MaxHealth;
        transform.position = respawnPosition;
        Respawned?.Invoke();
    }
}
