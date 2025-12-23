using UnityEngine;
using TMPro; // TextMeshPro icin gerekli

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Puanin yazacagi Text objesi")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;

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
        // Oyuna sifir puanla basla
        currentScore = 0;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            // Ornek cikti: "SCORE: 150"
            scoreText.text = "" + currentScore.ToString();
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}