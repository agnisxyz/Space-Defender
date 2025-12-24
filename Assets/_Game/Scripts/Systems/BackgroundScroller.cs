using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Tooltip("Arka planin akis hizi (0.1 - 0.5 arasi iyidir)")]
    [SerializeField] private float scrollSpeed = 0.2f;

    private Renderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Zamanla artan bir deger (Time.time) kullanarak Y eksenini kaydiriyoruz.
        // Wrap Mode: Repeat oldugu icin 1'i gecince otomatik 0'a doner, sonsuz dongu olur.
        float yOffset = Time.time * scrollSpeed;

        meshRenderer.material.mainTextureOffset = new Vector2(0, yOffset);
    }
}