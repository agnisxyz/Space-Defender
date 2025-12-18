using UnityEngine;

// Bu scriptin calismasi icin objede Rigidbody2D bileseni olmak zorundadir.
// Unity bunu otomatik ekler.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    // Geminin hareket hizi (Editor'den degistirilebilir)
    [SerializeField] private float moveSpeed = 10f;

    [Header("Boundaries")]
    // Ekranin kenarlarina ne kadar yaklasabilecegimizi belirleyen pay
    [SerializeField] private float xPadding = 0.6f;

    private Rigidbody2D _rb;
    private float _horizontalInput;
    private Vector2 _screenBounds;

    private void Awake()
    {
        // Obje uzerindeki Rigidbody2D bilesenine erisiyoruz
        _rb = GetComponent<Rigidbody2D>();

        // Yercekimi etkisini kapatiyoruz, uzaydayiz
        _rb.gravityScale = 0;

        // Carpismalari surekli kontrol et (Hizli hareket icin gerekli)
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Z ekseninde donmeyi engelle
        _rb.freezeRotation = true;

        // Y ekseninde hareketi fizigin kendisinde kilitliyoruz (Guvenlik onlemi)
        _rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    private void Start()
    {
        CalculateScreenBounds();
    }

    private void Update()
    {
        // 1. ADIM: Inputlari al (Her frame)
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        // 2. ADIM: Fizigi uygula (Sabit zaman araliginda)
        Move();
    }

    // --- METHODS ---

    // Ekran sinirlarini hesaplayan fonksiyon
    private void CalculateScreenBounds()
    {
        Camera mainCam = Camera.main;

        // Ekranin sag ust kosesinin dunya koordinatlarini aliyoruz
        // Bu sayede ekran genisligini dinamik olarak biliyoruz
        _screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
    }

    // Klavyeden girdileri okuyan fonksiyon
    private void ProcessInputs()
    {
        // Unity'nin eski input sistemi
        // Sol (-1), Durma (0), Sag (1)
        _horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    // Hareketi uygulayan fonksiyon
    private void Move()
    {
        // Su anki pozisyonu ve hiz faktorunu carparak yeni bir hiz vektoru olusturuyoruz
        // Time.fixedDeltaTime: Fizik motorunun zaman araligi (Akici hareket icin sart)
        Vector2 newPosition = _rb.position + new Vector2(_horizontalInput * moveSpeed * Time.fixedDeltaTime, 0f);

        // X pozisyonunu ekran sinirlarina hapsediyoruz (Clamping)
        // Sol sinir: -screenBounds.x + padding
        // Sag sinir: screenBounds.x - padding
        newPosition.x = Mathf.Clamp(newPosition.x, -_screenBounds.x + xPadding, _screenBounds.x - xPadding);

        // Rigidbody uzerinden pozisyonu guncelliyoruz
        _rb.MovePosition(newPosition);
    }
}