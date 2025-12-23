using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<int> OnEnergyChanged;

    [Header("Game Settings")]
    [SerializeField] private int maxEnergy = 100;

    [Header("References")]
    // Game Over scriptine erisim
    [SerializeField] private GameOverUI gameOverUI;

    public int CurrentEnergy { get; private set; }

    // Oyunun bitip bitmedigini kontrol eden bayrak
    private bool isGameOver = false;

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
        Time.timeScale = 1f; // Her ihtimale karsi zamani baslat

        if (OnEnergyChanged != null)
        {
            OnEnergyChanged.Invoke(CurrentEnergy);
        }
    }

    public void TakeDamage(int amount)
    {
        // Oyun zaten bittiyse daha fazla hasar alma
        if (isGameOver) return;

        CurrentEnergy -= amount;

        if (CurrentEnergy < 0) CurrentEnergy = 0;

        OnEnergyChanged?.Invoke(CurrentEnergy);

        if (CurrentEnergy <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        // Zamani durdur (Her sey donar)
        Time.timeScale = 0f;

        // Su anki puani al (ScoreManager'dan)
        int finalScore = 0;
        if (ScoreManager.Instance != null)
        {
            // ScoreManager'a "CurrentScore" diye bir property eklemedik, 
            // ama simdilik erismek yerine basitce ScoreManager'i public yapabiliriz 
            // ya da direkt ScoreManager singleton'indan cekebiliriz.
            // *Not: ScoreManager kodunda currentScore private oldugu icin erisemeyebiliriz.
            // PRATIK COZUM: ScoreManager'a bir "GetScore" metodu ekleyelim ya da public yapalim.
            // Simdilik 0 gonderiyorum, asagidaki notu oku.
            finalScore = ScoreManager.Instance.GetCurrentScore();
        }

        // UI'a haber ver
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver(finalScore);
        }
    }
}