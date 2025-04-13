using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float move = 10f; // T?c ?? di chuy?n
    [SerializeField] private float rotationalDamp = 5f; // ?? tr? quay (t?ng ?? quay nhanh h?n)
    [SerializeField] private float chaseRange = 30f; // Ph?m vi ?u?i theo
    private Transform target; // ??i t??ng m?c tiêu
    [SerializeField] private int damage = 10; // Sát th??ng m?i l?n ch?m
    [SerializeField] private Image healthBar; // Thanh máu hi?n th? UI
    private int maxHealth = 100; // Máu t?i ?a c?a nhân v?t
    private int currentHealth; // Máu hi?n t?i c?a nhân v?t

    void Start()
    {
        // Tìm ??i t??ng v?i tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }

        // Kh?i t?o máu cho nhân v?t
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= chaseRange) // ?u?i theo trong ph?m vi ?ã ??t
            {
                ChasePlayer();
            }
        }
    }

    void ChasePlayer()
    {
        Turn(); // ?i?u ch?nh h??ng v? phía ng??i ch?i
        Move(); // Ti?n v? phía tr??c
    }

    void Turn()
    {
        Vector3 direction = target.position - transform.position; // Tính h??ng t?i ng??i ch?i

        // N?u kho?ng cách nh?, không c?n quay
        if (direction.magnitude > 0f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction); // Quay v? phía ng??i ch?i
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime); // ?i?u ch?nh góc quay
        }
    }

    void Move()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // N?u quá g?n m?c tiêu, gi?m t?c ?? ?? không di chuy?n quá xa
        if (distanceToTarget > 1f)
        {
            transform.position += transform.forward * move * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u ??i t??ng va ch?m có tag "Player"
        if (other.CompareTag("Player"))
        {
            TakeDamage(damage); // Tr? máu c?a nhân v?t
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage; // Gi?m máu
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Gi?i h?n t? 0 ??n t?i ?a
        UpdateHealthBar(); // C?p nh?t thanh máu
        if (currentHealth <= 0)
        {
            Debug.Log("Player defeated!");
            Destroy(target.gameObject); // Xóa nhân v?t khi h?t máu
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // C?p nh?t thanh máu theo t? l?
        }
    }
}
