using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f; // Mermi 3 saniye sonra otamatik yok olsun (guvenlik icin)

    private float _lifeTimer;

    // Obje havuzdan her cikarildiginda (SetActive true oldugunda) bu fonksiyon calisir.
    // Start() yerine bunu kullaniyoruz.
    private void OnEnable()
    {
        _lifeTimer = lifeTime;
    }

    private void Update()
    {
        // 1. Mermiyi yukari hareket ettir
        // Vector2.up -> (0, 1) yani yukari yonu
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // 2. Omur suresini kontrol et
        _lifeTimer -= Time.deltaTime;

        if (_lifeTimer <= 0)
        {
            // Sure dolduysa objeyi yok etme, pasif yap (Havuza geri doner)
            gameObject.SetActive(false);
        }
    }

    // Ileride buraya carpisma (OnTriggerEnter2D) kodlarini ekleyecegiz.
}