using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    // Singleton ornegi. Her yerden erisilebilir.
    public static ObjectPoolManager Instance { get; private set; }

    // Inspector'da hangi objeden kac tane havuzlayacagimizi ayarladigimiz struct.
    [System.Serializable]
    public struct PoolItem
    {
        public PooledObjectType type;
        public GameObject prefab;
        public int initialAmount;
    }

    [Header("Pool Configuration")]
    [SerializeField] private List<PoolItem> poolItems;

    // Calisma zamaninda (Runtime) objeleri tuttugumuz sozluk.
    // Anahtar: Obje Turu (Enum), Deger: Obje Kuyrugu (Queue).
    private Dictionary<PooledObjectType, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        // Singleton yapisinin kurulmasi
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        _poolDictionary = new Dictionary<PooledObjectType, Queue<GameObject>>();

        foreach (PoolItem item in poolItems)
        {
            // Her tur icin bos bir kuyruk olustur
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < item.initialAmount; i++)
            {
                // Objeyi olustur ve sahne hiyerarsisinde duzenli durmasi icin bu objenin altina koy
                GameObject obj = Instantiate(item.prefab, transform);

                // Objeyi pasif yap (Havuza koy)
                obj.SetActive(false);

                // Kuyruga ekle
                objectQueue.Enqueue(obj);
            }

            // Sozluge kaydet
            _poolDictionary.Add(item.type, objectQueue);
        }
    }

    // Havuzdan obje isteyen metod
    public GameObject GetPooledObject(PooledObjectType type, Vector2 position, Quaternion rotation)
    {
        // Istenen turde bir havuz var mi kontrol et
        if (!_poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Pool with type {type} doesn't exist!");
            return null;
        }

        // Kuyruktaki ilk objeyi al
        GameObject objectToSpawn = _poolDictionary[type].Dequeue();

        // Eger aldigimiz obje zaten aktifse (Havuz yetersiz kaldiysa ve donguye girdiyse)
        // veya havuz bosaldiysa, dinamik olarak yeni bir tane olustur (Lazy instantiation).
        if (objectToSpawn.activeInHierarchy)
        {
            // Not: Normalde buraya dinamik genisleme kodu yazilir ama simdilik basit tutalim.
            // Kuyrugun sonuna geri ekleyip, yeni bir kopya olusturalim.
            _poolDictionary[type].Enqueue(objectToSpawn);

            // Orijinal prefab'i bulmamiz lazim (Performans icin bu kisim ileride optimize edilebilir)
            GameObject prefab = GetPrefabByType(type);
            objectToSpawn = Instantiate(prefab, transform);
        }

        // Objeyi aktif et ve pozisyonunu ayarla
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // Isimiz bitince (ornegin mermi ekrandan cikinca) tekrar kullanmak uzere kuyrugun sonuna ekle
        _poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    // Helper: Turune gore prefab'i bulmak icin (Sadece havuz genisletilirken kullanilir)
    private GameObject GetPrefabByType(PooledObjectType type)
    {
        foreach (var item in poolItems)
        {
            if (item.type == type) return item.prefab;
        }
        return null;
    }
}