using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton ?? truy c?p to�n c?c

    [SerializeField] private Slider healthSlider; // Thanh m�u
    [SerializeField] private Slider mpSlider; // Thanh MP
    [SerializeField] private GameObject winLosePanel; // B?ng th�ng b�o th?ng/thua
    [SerializeField] private TextMeshProUGUI winLoseText; // V?n b?n th�ng b�o th?ng/thua

    private void Awake()
    {
        // Thi?t l?p Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Lo?i b? c�c instance tr�ng l?p
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / 100; // C?p nh?t thanh m�u
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
            winLosePanel.SetActive(true); // Hi?n th? b?ng th�ng b�o
        }

        if (winLoseText != null)
        {
            winLoseText.text = message; // C?p nh?t n?i dung th�ng b�o
        }
    }
}
