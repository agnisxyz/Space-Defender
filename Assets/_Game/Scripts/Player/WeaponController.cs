using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Gun Settings")]
    [Tooltip("Saniyede kac saniye arayla ates edilecek")]
    [SerializeField] private float fireRate = 0.2f; // Ne kadar kucukse o kadar hizli atar

    [Header("References")]
    [Tooltip("Merminin cikacagi nokta")]
    [SerializeField] private Transform firePoint;

    private float _fireTimer;

    private void Update()
    {
        // Otomatik Ates Sistemi
        // Zamanlayiciyi artir
        _fireTimer += Time.deltaTime;

        // Zamanlayici belirlenen sureyi gecti mi?
        if (_fireTimer >= fireRate)
        {
            Shoot();

            // Ates ettikten sonra zamanlayiciyi sifirla
            _fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        // Singleton yapisina erisip mermi istiyoruz.
        // Hangi tur? -> PlayerBullet
        // Nereye? -> firePoint.position (Namlunun ucu)
        // Hangi yon? -> Quaternion.identity (Donus yok, duz)

        ObjectPoolManager.Instance.GetPooledObject(
            PooledObjectType.PlayerBullet,
            firePoint.position,
            Quaternion.identity
        );
    }
}