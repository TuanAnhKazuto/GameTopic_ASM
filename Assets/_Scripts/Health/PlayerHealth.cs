using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Máu t?i ?a
    private int currentHealth; // Máu hi?n t?i
    [SerializeField] private Image healthBar; // Thanh máu (UI)
    [SerializeField] private TextMeshProUGUI healthText; // V?n b?n hi?n th? máu (UI)

    void Start()
    {
        currentHealth = maxHealth; // Kh?i t?o máu ??y ??
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Tr? máu
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ??m b?o máu không nh? h?n 0
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Player has been defeated!");
            // Thêm hành ??ng khác nh? k?t thúc trò ch?i, h?i sinh, v.v.
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // C?p nh?t thanh máu
        }
        if (healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}"; // Hi?n th? máu d??i d?ng v?n b?n
        }
    }
}
