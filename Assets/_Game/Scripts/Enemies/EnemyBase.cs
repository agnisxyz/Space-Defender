using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected int maxHealth = 1;
    [SerializeField] protected int damagetoBase = 10; // Kacarsa uste verilecek hasar

    protected int currentHealth;

    // OnEnable: Obje havuzdan her ciktiginda calisir (Start yerine gecer)
    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        // 1. Hareketi yap (Cocuklar bunu degistirebilir)
        Move();

        // 2. Sinir kontrolu (Ekrandan cikti mi?)
        CheckOutOfBounds();
    }

    // "virtual" demek: Cocuk siniflar bu metodu kendine gore degistirebilir (Override)
    protected virtual void Move()
    {
        // Varsayilan hareket: Dumduz asagi
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y < -6f) // Ekranin alti
        {
            // TODO: Ileride burada GameManager.Instance.TakeDamage(damageToBase) cagiracagiz.
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
        // TODO: Ileride buraya patlama efekti ve ses ekleyecegiz.
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    // Carpisma mantigi
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eger mermi carparsa
        if (other.CompareTag("PlayerBullet"))
        {
            // Mermiyi havuza gonder
            other.gameObject.SetActive(false);

            // Hasar al
            TakeDamage(1);
        }
        // Eger oyuncuya carparsa
        else if (other.CompareTag("Player"))
        {
            // TODO: Oyuncuya hasar ver
            ReturnToPool();
        }
    }
}