using UnityEngine;
using UnityEngine.UI; // UI elemanlari icin (Slider)
using TMPro; // TextMeshPro icin (Yazilar)

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Enerji bari (Slider)")]
    [SerializeField] private Slider energyBar;

    [Tooltip("Enerji yuzdesini gosteren yazi")]
    [SerializeField] private TextMeshProUGUI energyText;

    private void Start()
    {
        // GameManager'daki olaya "Abone Oluyoruz" (Subscribe).
        // Yani: "Enerji degisince benim UpdateEnergyUI fonksiyonumu calistir."
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnEnergyChanged += UpdateEnergyUI;
        }
    }

    private void OnDestroy()
    {
        // Obje yok olurken aboneligi iptal etmeliyiz. (Memory Leak onlemi)
        // Senior Dev kuralidir: Actigin muslugu kapat.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnEnergyChanged -= UpdateEnergyUI;
        }
    }

    // Bu fonksiyon GameManager tarafindan tetiklenecek
    private void UpdateEnergyUI(int currentEnergy)
    {
        // Slider degerini guncelle
        if (energyBar != null)
        {
            energyBar.value = currentEnergy;
        }

        // Yaziyi guncelle
        if (energyText != null)
        {
            energyText.text = currentEnergy.ToString() + "%";
        }
    }
}