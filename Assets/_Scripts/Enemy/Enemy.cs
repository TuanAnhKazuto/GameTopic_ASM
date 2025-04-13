using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float move = 10f; // T?c ?? di chuy?n
    [SerializeField] private float rotationalDamp = 5f; // ?? tr? quay
    [SerializeField] private float chaseRange = 30f; // Ph?m vi ?u?i theo
    [SerializeField] private Transform defaultTarget; // M?c tiêu m?c ??nh khi ch?a có Player
    [SerializeField] private Image healthBar; // Thanh máu c?a k? ??ch
    private Transform target; // ??i t??ng m?c tiêu
    private int maxHealth = 100; // Máu t?i ?a c?a k? ??ch
    private int currentHealth; // Máu hi?n t?i c?a k? ??ch

    [SerializeField] private GameObject attackArea; // Empty GameObject dùng ?? t?n công

    void Start()
    {
        // Gán m?c tiêu m?c ??nh n?u Player ch?a xu?t hi?n
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform; // Gán m?c tiêu là Player n?u xu?t hi?n
        }
        else if (defaultTarget != null)
        {
            target = defaultTarget; // Gán m?c tiêu m?c ??nh ???c ??t tr??c
            Debug.LogWarning("Player not found! Enemy is chasing the default target.");
        }
        else
        {
            Debug.LogError("No Player or default target found! Enemy will stay idle.");
        }

        // Kh?i t?o máu cho k? ??ch
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Ki?m tra s? xu?t hi?n c?a Player trong game
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform; // C?p nh?t m?c tiêu khi Player xu?t hi?n
        }

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // ?u?i theo m?c tiêu n?u trong ph?m vi ho?c s? d?ng defaultTarget
            if (distanceToTarget <= chaseRange || target == defaultTarget)
            {
                ChaseTarget();
            }
        }
    }

    void ChaseTarget()
    {
        Turn(); // ?i?u ch?nh h??ng
        Move(); // Ti?n v? phía m?c tiêu
    }

    void Turn()
    {
        Vector3 direction = target.position - transform.position; // Tính h??ng t?i m?c tiêu
        if (direction.magnitude > 0f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction); // Xoay h??ng v? phía m?c tiêu
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime); // Làm m??t góc quay
        }
    }

    void Move()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Ti?n g?n t?i m?c tiêu n?u còn kho?ng cách
        if (distanceToTarget > 1f)
        {
            transform.position += transform.forward * move * Time.deltaTime;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // C?p nh?t thanh máu theo t? l?
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u ??i t??ng va ch?m là Bullet
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by Bullet! Destroying enemy.");
            Destroy(gameObject); // Xóa k? ??ch
        }

        // Ki?m tra n?u ??i t??ng va ch?m là Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Tr? 10 máu c?a Player
                Debug.Log("Player hit by attack! Player health reduced by 10.");
            }
        }
    }
}
