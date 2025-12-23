using UnityEngine;
using UnityEngine.SceneManagement; // Sahneyi yeniden yuklemek icin sart
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Oyun bitince puani gosterecek yazi")]
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Tooltip("Game Over panelinin kendisi (Acip kapatmak icin)")]
    [SerializeField] private GameObject contentPanel;

    private void Start()
    {
        // Oyun baslarken paneli gizle
        if (contentPanel != null)
        {
            contentPanel.SetActive(false);
        }
    }

    // Bu fonksiyonu GameManager cagiracak
    public void ShowGameOver(int score)
    {
        // Paneli ac
        contentPanel.SetActive(true);

        // Son puani yaz
        if (finalScoreText != null)
        {
            finalScoreText.text = "SCORE: " + score.ToString();
        }
    }

    // Butona baglayacagimiz fonksiyon
    public void RestartGame()
    {
        // Zamani tekrar normal akisina dondur (Cunku Game Over'da durdurmustuk)
        Time.timeScale = 1f;

        // Sahneyi bastan yukle
        // SceneManager.GetActiveScene().buildIndex -> Su anki sahnenin numarasi
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Butona baglayacagimiz fonksiyon
    public void QuitGame()
    {
        Debug.Log("Oyundan cikiliyor...");
        Application.Quit();
    }
}