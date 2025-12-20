using UnityEngine;
using System; // Action kullanmak icin gerekli kutuphane

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // UI veya baska sistemlerin dinleyebilecegi bir "Olay" (Event) tanimliyoruz.
    // Bu olay, tetiklendiginde yaninda bir tamsayi (int) tasiyacak.
    public event Action<int> OnEnergyChanged;

    [Header("Game Settings")]
    [Tooltip("Baslangic enerjimiz (Canimiz)")]
    [SerializeField] private int maxEnergy = 100;

    public int CurrentEnergy { get; private set; }

    private void Awake()
    {
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
        CurrentEnergy = maxEnergy;

        // Oyun baslar baslamaz UI'in guncellenmesi icin olayi bir kez tetikliyoruz.
        if (OnEnergyChanged != null)
        {
            OnEnergyChanged.Invoke(CurrentEnergy);
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentEnergy -= amount;

        if (CurrentEnergy < 0) CurrentEnergy = 0;

        // Enerji degisti! Dinleyen herkese (UI) haber ver.
        // ?.Invoke su demektir: "Eger dinleyen varsa calistir, yoksa hata verme."
        OnEnergyChanged?.Invoke(CurrentEnergy);

        if (CurrentEnergy <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }
}