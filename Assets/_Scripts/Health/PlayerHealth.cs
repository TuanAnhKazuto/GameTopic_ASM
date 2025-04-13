using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // M�u t?i ?a
    private int currentHealth; // M�u hi?n t?i
    [SerializeField] private Image healthBar; // Thanh m�u (UI)
    [SerializeField] private TextMeshProUGUI healthText; // V?n b?n hi?n th? m�u (UI)

    void Start()
    {
        currentHealth = maxHealth; // Kh?i t?o m�u ??y ??
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Tr? m�u
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ??m b?o m�u kh�ng nh? h?n 0
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Player has been defeated!");
            // Th�m h�nh ??ng kh�c nh? k?t th�c tr� ch?i, h?i sinh, v.v.
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // C?p nh?t thanh m�u
        }
        if (healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}"; // Hi?n th? m�u d??i d?ng v?n b?n
        }
    }
}
