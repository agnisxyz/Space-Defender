using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton: Her yerden erisilebilir tek patron
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [Tooltip("Baslangic enerjimiz (Canimiz)")]
    [SerializeField] private int maxEnergy = 100;

    // Oyundaki anlik enerji durumumuz
    // Sadece buradan degistirilsin (private set), disaridan sadece okunsun (public get)
    public int CurrentEnergy { get; private set; }

    private void Awake()
    {
        // Singleton Kurulumu
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Oyuna tam canla basla
        CurrentEnergy = maxEnergy;
        Debug.Log("Oyun Basladi! Enerji: " + CurrentEnergy);
    }

    // Hasar alma fonksiyonu (Disaridan cagirilacak)
    public void TakeDamage(int amount)
    {
        CurrentEnergy -= amount;

        // Eksiye dusmesin diye 0'a sabitliyoruz
        if (CurrentEnergy < 0) CurrentEnergy = 0;

        Debug.Log($"Hasar Alindi: -{amount}. Kalan Enerji: {CurrentEnergy}");

        if (CurrentEnergy <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("OYUN BITTI! (GAME OVER)");
        // Ileride buraya "Yeniden Basla" ekranini acan kodu yazacagiz.
        // Simdilik oyunu donduralim.
        Time.timeScale = 0f;
    }
}