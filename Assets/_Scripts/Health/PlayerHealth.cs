using UnityEngine;
using UnityEngine.UI; // Th? vi?n UI ?? l�m vi?c v?i Image

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // M�u t?i ?a
    private int currentHealth; // M�u hi?n t?i
    [SerializeField] private Image healthBar; // Tham chi?u ??n Image l�m thanh m�u

    void Start()
    {
        currentHealth = maxHealth; // Kh?i t?o m�u
        UpdateHealthBar(); // C?p nh?t thanh m�u ngay khi b?t ??u
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attack")) // Ki?m tra va ch?m v?i qu�i
        {
            TakeDamage(10); // Tr? 10 m�u
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage; // Gi?m m�u
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Gi?i h?n gi� tr? m�u t? 0 ??n maxHealth
        UpdateHealthBar(); // C?p nh?t thanh m�u
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Khi h?t m�u, x�a nh�n v?t
            Debug.Log("Player has been defeated!");
        }
    }

    private void UpdateHealthBar()
    {
        // Thay ??i k�ch th??c Image theo t? l? m�u
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        // `fillAmount` ph?i l� gi� tr? t? 0 ??n 1
    }
}
