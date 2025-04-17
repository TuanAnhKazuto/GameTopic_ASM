using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // M�u t?i ?a
    private int currentHealth; // M�u hi?n t?i
    [SerializeField] private int damageFromBullet = 10; // L??ng m�u b? tr? khi tr�ng ??n

    void Start()
    {
        // Kh?i t?o m�u ??y ??
        currentHealth = maxHealth;

        // C?p nh?t UI kh?i ??ng (c?p nh?t thanh m�u)
        UIManager.Instance.UpdateHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u va ch?m v?i ??i t??ng c� tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(damageFromBullet); // Tr? m�u
            Debug.Log($"Player b? b?n! M�u gi?m {damageFromBullet}.");
        }
    }

    public void TakeDamage(int damage)
    {
        // Tr? m�u v� gi?i h?n kh�ng nh? h?n 0
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // C?p nh?t thanh m�u trong UI
        UIManager.Instance.UpdateHealth(currentHealth);

        // X? l� khi m�u b?ng 0
        if (currentHealth <= 0)
        {
            Debug.Log("Ng??i ch?i ?� b? ?�nh b?i!");
            HandlePlayerDeath();
        }
    }

    private void HandlePlayerDeath()
    {
        // Hi?n th? th�ng b�o thua
        UIManager.Instance.ShowWinLoseMessage("You Lose!");
        Debug.Log("K?t th�c tr� ch?i.");
    }

    

}
