using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Máu t?i ?a
    private int currentHealth; // Máu hi?n t?i
    [SerializeField] private int damageFromBullet = 10; // L??ng máu b? tr? khi trúng ??n

    void Start()
    {
        // Kh?i t?o máu ??y ??
        currentHealth = maxHealth;

        // C?p nh?t UI kh?i ??ng (c?p nh?t thanh máu)
        UIManager.Instance.UpdateHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u va ch?m v?i ??i t??ng có tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(damageFromBullet); // Tr? máu
            Debug.Log($"Player b? b?n! Máu gi?m {damageFromBullet}.");
        }
    }

    public void TakeDamage(int damage)
    {
        // Tr? máu và gi?i h?n không nh? h?n 0
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // C?p nh?t thanh máu trong UI
        UIManager.Instance.UpdateHealth(currentHealth);

        // X? lý khi máu b?ng 0
        if (currentHealth <= 0)
        {
            Debug.Log("Ng??i ch?i ?ã b? ?ánh b?i!");
            HandlePlayerDeath();
        }
    }

    private void HandlePlayerDeath()
    {
        // Hi?n th? thông báo thua
        UIManager.Instance.ShowWinLoseMessage("You Lose!");
        Debug.Log("K?t thúc trò ch?i.");
    }

    

}
