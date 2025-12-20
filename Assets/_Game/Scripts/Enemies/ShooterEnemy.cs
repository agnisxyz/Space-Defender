using UnityEngine;

public class ShooterEnemy : EnemyBase
{
    [Header("Shooting Settings")]
    [Tooltip("Kac saniyede bir ates edecek")]
    [SerializeField] private float fireRate = 2f;

    // Merminin cikacagi nokta (Opsiyonel, yoksa govdeden cikar)
    [SerializeField] private Transform firePoint;

    private float fireTimer;

    protected override void Update()
    {
        // 1. Standart hareket (Base siniftan geliyor)
        base.Update();

        // 2. Ates etme mantigi
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        // Eger firePoint atanmamissa kendi pozisyonunu kullan
        Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;

        // Havuzdan mermi iste
        if (ObjectPoolManager.Instance != null)
        {
            ObjectPoolManager.Instance.GetPooledObject(
                PooledObjectType.EnemyBullet,
                spawnPos,
                Quaternion.identity
            );
        }
    }
}