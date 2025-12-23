using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float followSpeed = 15f;

    [Header("Screen Bounds (Sinirlar)")]
    // X ekseni icin (Sol Sinir, Sag Sinir)
    [SerializeField] private Vector2 xLimits = new Vector2(-2.2f, 2.2f);

    // Y ekseni icin (Alt Sinir, Ust Sinir)
    [SerializeField] private Vector2 yLimits = new Vector2(-4f, 4f);

    private Camera mainCamera;
    private Vector3 targetPos;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveWithMouse();
    }

    private void MoveWithMouse()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        targetPos.x = Mathf.Clamp(mouseWorldPos.x, xLimits.x, xLimits.y);
        targetPos.y = Mathf.Clamp(mouseWorldPos.y, yLimits.x, yLimits.y);
        targetPos.z = 0f;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    // --- YENI EKLENEN KISIM ---
    // Bu fonksiyon sadece Scene ekraninda cizim yapar, oyunda gorunmez.
    // Sinirlari kirmizi bir kutu olarak gormeni saglar.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Kutunun merkezini ve boyutunu hesapla
        float width = xLimits.y - xLimits.x;
        float height = yLimits.y - yLimits.x;
        float centerX = (xLimits.x + xLimits.y) / 2;
        float centerY = (yLimits.x + yLimits.y) / 2;

        Gizmos.DrawWireCube(new Vector3(centerX, centerY, 0), new Vector3(width, height, 1));
    }
}