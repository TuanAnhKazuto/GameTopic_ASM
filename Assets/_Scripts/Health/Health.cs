using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public Image healthImage; // Thanh m�u s? d?ng Image
    public TextMeshProUGUI healthText; // Text hi?n th?

    private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // G�n m�u ban ??u

        // Ki?m tra xem c�c th�nh ph?n ?� ???c g�n ch?a
        if (healthImage == null)
        {
            Debug.LogError("Health Image is not assigned in the Inspector!");
        }

        if (healthText == null)
        {
            Debug.LogError("Health Text is not assigned in the Inspector!");
        }

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        // Ki?m tra t?ng th�nh ph?n tr??c khi s? d?ng
        if (healthImage != null)
        {
            healthImage.fillAmount = (float)currentHealth / maxHealth; // C?p nh?t m?c ?? ??y c?a thanh m�u
        }
        else
        {
            Debug.LogWarning("Health Image is null. Please assign it in the Inspector.");
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}"; // C?p nh?t ch?
        }
        else
        {
            Debug.LogWarning("Health Text is null. Please assign it in the Inspector.");
        }
    }
}
