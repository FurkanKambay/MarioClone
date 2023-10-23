using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action BulletFired;

    public float BulletSpeed = 10f;
    public float FireRate = 1f;

    [SerializeField] private GameObject bulletPrefab;

    private float timeSinceLastFire = 0f;

    private void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (timeSinceLastFire < FireRate)
                return;
            Shoot();
        }
    }

    private void Shoot()
    {
        timeSinceLastFire = 0f;
        Transform instance = Instantiate(bulletPrefab, transform.position, Quaternion.identity).transform;
        instance.position = transform.position;
        instance.localScale = transform.parent.localScale;

        Bullet bullet = instance.GetComponent<Bullet>();
        bullet.Speed = BulletSpeed;

        BulletFired?.Invoke();
    }
}
