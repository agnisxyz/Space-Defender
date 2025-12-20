using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    // Hangi dusman turunu uretecegimizi seciyoruz
    [SerializeField] private PooledObjectType enemyType = PooledObjectType.ScoutEnemy;

    [Tooltip("Kac saniyede bir dusman gelecek")]
    [SerializeField] private float spawnRate = 1.5f;

    [Tooltip("Yatay eksende rastgele dogus araligi")]
    [SerializeField] private float xSpawnRange = 2.5f;

    [Tooltip("Dikey eksende dogus yuksekligi")]
    [SerializeField] private float ySpawnPosition = 6f;

    // Alt tire kaldirildi
    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        Vector2 spawnPos = new Vector2(randomX, ySpawnPosition);

        // Object Pool Manager uzerinden dusman istiyoruz
        ObjectPoolManager.Instance.GetPooledObject(enemyType, spawnPos, Quaternion.identity);
    }
}