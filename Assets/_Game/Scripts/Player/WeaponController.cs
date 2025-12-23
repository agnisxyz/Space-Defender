using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Gun Settings")]
    [Tooltip("Saniyede kac saniye arayla ates edilecek (Orn: 0.2 seri atis)")]
    [SerializeField] private float fireRate = 0.2f;

    [Header("References")]
    [Tooltip("Merminin cikacagi nokta")]
    [SerializeField] private Transform firePoint;

    private float fireTimer;

    private void Update()
    {
        // 1. Ates etme zamanlayicisi surekli saysin
        fireTimer += Time.deltaTime;

        // 2. KONTROL:
        // Input.GetMouseButton(0) -> Sol tik BASILI TUTULDUGU SURECE true doner.
        // fireTimer >= fireRate -> Silahin soguma suresi doldu mu?
        if (Input.GetMouseButton(0) && fireTimer >= fireRate)
        {
            Shoot();

            // Ates ettikten sonra sayaci sifirla
            fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        if (ObjectPoolManager.Instance != null)
        {
            ObjectPoolManager.Instance.GetPooledObject(
                PooledObjectType.PlayerBullet,
                firePoint.position,
                Quaternion.identity
            );
        }
    }
}