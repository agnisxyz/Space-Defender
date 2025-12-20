using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Settings")]
    [Tooltip("Dusmanin hareket hizi")]
    [SerializeField] protected float moveSpeed = 2f;

    [Tooltip("Dusmanin maksimum cani")]
    [SerializeField] protected int maxHealth = 1;

    [Tooltip("Dusman asagidan kacarsa veya bize carparsa enerjimizden ne kadar gidecek")]
    [SerializeField] protected int damageToBase = 10;

    // Anlik cani tutan degisken
    protected int currentHealth;

    // Obje havuzdan her ciktiginda (Spawn oldugunda) calisir
    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        // 1. Hareket mantigi (Cocuk siniflar bunu degistirebilir)
        Move();

        // 2. Sinir kontrolu
        CheckOutOfBounds();
    }

    // Standart hareket: Dumduz asagi
    protected virtual void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void CheckOutOfBounds()
    {
        // Ekranin alt sinirindan (Y: -6) cikarsa
        if (transform.position.y < -6f)
        {
            // GameManager'a haber ver ve hasar uygula
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage(damageToBase);
            }

            // Dusmani havuza geri gonder
            ReturnToPool();
        }
    }

    // Disaridan (Mermiden vs.) hasar alma fonksiyonu
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Ileride buraya patlama efekti (Particle) ve ses eklenecek
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    // Carpisma Algilama
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Eger bize Mermi carparsa
        if (other.CompareTag("PlayerBullet"))
        {
            // Mermiyi yok etme, havuza geri gonder
            other.gameObject.SetActive(false);

            // Hasar al
            TakeDamage(1);
        }
        // 2. Eger OYUNCUYA carparsa (Kamikaze)
        else if (other.CompareTag("Player"))
        {
            // GameManager'a haber ver.
            // Dogrudan carpisma oldugu icin ceza olarak 2 kat hasar veriyoruz.
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage(damageToBase * 2);
            }

            // Dusman carpmanin etkisiyle yok olur
            Die();
        }
    }
}