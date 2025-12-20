using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Kac saniyede bir dusman gelecek")]
    [SerializeField] private float spawnRate = 1.5f;

    [Tooltip("Yatay eksende rastgele dogus araligi")]
    [SerializeField] private float xSpawnRange = 1.8f;

    [Tooltip("Dikey eksende dogus yuksekligi")]
    [SerializeField] private float ySpawnPosition = 6f;

    // Tek bir tur yerine, ihtimalleri dizi olarak tutuyoruz
    private PooledObjectType[] enemyTypes;
    private float spawnTimer;

    private void Start()
    {
        // Spawner'in uretebilecegi dusman listesini hazirliyoruz
        enemyTypes = new PooledObjectType[]
        {
            PooledObjectType.ScoutEnemy,
            PooledObjectType.HeavyEnemy,
            PooledObjectType.ShooterEnemy
        };
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            SpawnRandomEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnRandomEnemy()
    {
        // Rastgele bir pozisyon
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        Vector2 spawnPos = new Vector2(randomX, ySpawnPosition);

        // Rastgele bir dusman turu sec (0, 1 veya 2)
        int randomIndex = Random.Range(0, enemyTypes.Length);
        PooledObjectType selectedType = enemyTypes[randomIndex];

        // Secilen dusmani havuzdan cagir
        if (ObjectPoolManager.Instance != null)
        {
            ObjectPoolManager.Instance.GetPooledObject(selectedType, spawnPos, Quaternion.identity);
        }
    }
}