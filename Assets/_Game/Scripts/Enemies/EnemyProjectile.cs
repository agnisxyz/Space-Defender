using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 4f;

    private float lifeTimer;

    private void OnEnable()
    {
        lifeTimer = lifeTime;
    }

    private void Update()
    {
        // Asagi dogru hareket (Vector2.down)
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Omur suresi kontrolu
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eger mermi Oyuncuya (Player) carparsa
        if (other.CompareTag("Player"))
        {
            // 1. Oyuncuya hasar ver (GameManager uzerinden)
            // Ornek: 10 hasar
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage(10);
            }

            // 2. Mermiyi yok et (Havuza gonder)
            gameObject.SetActive(false);
        }
    }
}