using UnityEngine;

public class ScoutEnemy : EnemyBase
{
    [Header("Zigzag Settings")]
    [SerializeField] private float frequency = 5f;
    [SerializeField] private float magnitude = 5f;

    // Her dusman icin ozel, rastgele bir baslangic zamani tutacak degisken
    private float timeOffset;

    // Havuzdan her ciktiginda calisir
    protected override void OnEnable()
    {
        // Baba sinifin (EnemyBase) OnEnable'ini calistir (Canlari fullesin vs.)
        base.OnEnable();

        // 0 ile 20 arasinda rastgele bir sayi sec ve hafizaya at
        // Bu sayede her dusmanin "kendi saati" farkli olacak
        timeOffset = Random.Range(0f, 20f);
    }

    protected override void Move()
    {
        Vector3 pos = transform.position;

        // 1. Asagi hareket
        pos.y -= moveSpeed * Time.deltaTime;

        // 2. Yatay Hareket
        // Time.time'a kendi ozel offset degerimizi ekliyoruz.
        // Artik hepsi farkli bir ritimde hareket edecek.
        pos.x += Mathf.Sin((Time.time + timeOffset) * frequency) * magnitude * Time.deltaTime;

        transform.position = pos;
    }
}