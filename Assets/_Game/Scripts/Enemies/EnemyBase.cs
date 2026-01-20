using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Settings")]
    [Tooltip("Dusmanin hareket hizi")]
    [SerializeField] protected float moveSpeed = 2f;

    [Tooltip("Dusmanin cani")]
    [SerializeField] protected int maxHealth = 1;

    [Tooltip("Kacarsa veya carparsa base'e verilecek hasar")]
    [SerializeField] protected int damageToBase = 10;

    [Tooltip("Oldugunde verecegi puan")]
    [SerializeField] protected int scoreValue = 10; // Puan degiskeni

    protected int currentHealth;

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        Move();
        CheckOutOfBounds();
    }

    protected virtual void Move()
    {
        // Varsayilan hareket: Dumduz asagi
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void CheckOutOfBounds()
    {
        // Ekranin altindan (Y: -6) cikarsa
        if (transform.position.y < -6f)
        {
            // GameManager'a hasar bilgisini gonder
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage(damageToBase);
            }

            ReturnToPool();
        }
    }

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


        // 1. Patlama Efektini Cagır
        // "Explosion" turundeki objeyi, benim oldugum yerde (transform.position) olustur.
        ObjectPoolManager.Instance.GetPooledObject(PooledObjectType.Explosion, transform.position, Quaternion.identity);

        // 2. Düşmanı Kapat
        gameObject.SetActive(false);
        // YENI: Olmeden once puani ver
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (transform.position.y > 5) return;
        // Mermi carparsa
        if (other.CompareTag("PlayerBullet"))
        {
            other.gameObject.SetActive(false); // Mermiyi havuza yolla
            TakeDamage(1);
        }
        // Oyuncuya carparsa
        else if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                // Carpma cezasi olarak 2 kat hasar
                GameManager.Instance.TakeDamage(damageToBase * 2);
            }

            Die();
        }
    }
}