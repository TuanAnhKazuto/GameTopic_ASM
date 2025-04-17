using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton ?? truy c?p toàn c?c

    [SerializeField] private Slider healthSlider; // Thanh máu
    [SerializeField] private Slider mpSlider; // Thanh MP
    [SerializeField] private GameObject winLosePanel; // B?ng thông báo th?ng/thua
    [SerializeField] private TextMeshProUGUI winLoseText; // V?n b?n thông báo th?ng/thua

    private void Awake()
    {
        // Thi?t l?p Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Lo?i b? các instance trùng l?p
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / 100; // C?p nh?t thanh máu
        }
    }

    public void UpdateMP(int currentMP)
    {
        if (mpSlider != null)
        {
            mpSlider.value = (float)currentMP / 100; // C?p nh?t thanh MP
        }
    }

    public void ShowWinLoseMessage(string message)
    {
        if (winLosePanel != null)
        {
            winLosePanel.SetActive(true); // Hi?n th? b?ng thông báo
        }

        if (winLoseText != null)
        {
            winLoseText.text = message; // C?p nh?t n?i dung thông báo
        }
    }
}
