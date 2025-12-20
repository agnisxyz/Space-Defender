using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Gun Settings")]
    [Tooltip("Saniyede kac saniye arayla ates edilecek")]
    [SerializeField] private float fireRate = 0.2f;

    [Header("References")]
    [Tooltip("Merminin cikacagi nokta")]
    [SerializeField] private Transform firePoint;

    private float fireTimer;

    private void Update()
    {
        // --- TEST MODU (MANUEL ATES) ---
        // Sadece Space tusuna basildiginda ates et.
        // GetKeyDown: Tusa bastigin an 1 kere calisir.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        // --- NORMAL MOD (OTOMATIK ATES) ---
        // Test bitince yukaridaki kodu silip buradaki yorum satirlarini kaldir.
        /*
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            
            // Ates ettikten sonra zamanlayiciyi sifirla
            fireTimer = 0f;
        }
        */
    }

    private void Shoot()
    {
        // Singleton yapisina erisip mermi istiyoruz.
        // Hata olmamasi icin null kontrolu yapiyoruz (Best Practice).
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