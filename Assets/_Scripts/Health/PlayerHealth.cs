using UnityEngine;
using UnityEngine.UI; // Th? vi?n UI ?? làm vi?c v?i Image

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Máu t?i ?a
    private int currentHealth; // Máu hi?n t?i
    [SerializeField] private Image healthBar; // Tham chi?u ??n Image làm thanh máu

    void Start()
    {
        currentHealth = maxHealth; // Kh?i t?o máu
        UpdateHealthBar(); // C?p nh?t thanh máu ngay khi b?t ??u
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attack")) // Ki?m tra va ch?m v?i quái
        {
            TakeDamage(10); // Tr? 10 máu
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage; // Gi?m máu
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Gi?i h?n giá tr? máu t? 0 ??n maxHealth
        UpdateHealthBar(); // C?p nh?t thanh máu
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Khi h?t máu, xóa nhân v?t
            Debug.Log("Player has been defeated!");
        }
    }

    private void UpdateHealthBar()
    {
        // Thay ??i kích th??c Image theo t? l? máu
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        // `fillAmount` ph?i là giá tr? t? 0 ??n 1
    }
}
