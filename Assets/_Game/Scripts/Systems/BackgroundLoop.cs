using UnityEngine;

public class SimpleBackgroundLoop : MonoBehaviour
{
    [Tooltip("Asagi kayma hizi")]
    [SerializeField] private float speed = 4f;

    private float height;

    private void Start()
    {
        // Resmin yüksekligini otomatik al
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        // Asagi hareket
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Eger tamamen asagi indiysem (ekrandan ciktiysam)
        if (transform.position.y < -height)
        {
            // En tepeye (diger resmin ustune) isinlan
            Vector3 resetPos = transform.position;
            resetPos.y += 2f * height;
            transform.position = resetPos;
        }
    }
}