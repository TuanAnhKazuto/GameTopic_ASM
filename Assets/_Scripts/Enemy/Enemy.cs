using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float move = 10f; // T?c ?? di chuy?n
    [SerializeField] private float rotationalDamp = 5f; // ?? tr? quay
    [SerializeField] private float chaseRange = 30f; // Ph?m vi ?u?i theo
    [SerializeField] private Transform defaultTarget; // M?c ti�u m?c ??nh khi ch?a c� Player
    [SerializeField] private Image healthBar; // Thanh m�u c?a k? ??ch
    private Transform target; // ??i t??ng m?c ti�u
    private int maxHealth = 100; // M�u t?i ?a c?a k? ??ch
    private int currentHealth; // M�u hi?n t?i c?a k? ??ch

    [SerializeField] private GameObject attackArea; // Empty GameObject d�ng ?? t?n c�ng

    void Start()
    {
        // G�n m?c ti�u m?c ??nh n?u Player ch?a xu?t hi?n
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform; // G�n m?c ti�u l� Player n?u xu?t hi?n
        }
        else if (defaultTarget != null)
        {
            target = defaultTarget; // G�n m?c ti�u m?c ??nh ???c ??t tr??c
            Debug.LogWarning("Player not found! Enemy is chasing the default target.");
        }
        else
        {
            Debug.LogError("No Player or default target found! Enemy will stay idle.");
        }

        // Kh?i t?o m�u cho k? ??ch
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Ki?m tra s? xu?t hi?n c?a Player trong game
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform; // C?p nh?t m?c ti�u khi Player xu?t hi?n
        }

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // ?u?i theo m?c ti�u n?u trong ph?m vi ho?c s? d?ng defaultTarget
            if (distanceToTarget <= chaseRange || target == defaultTarget)
            {
                ChaseTarget();
            }
        }
    }

    void ChaseTarget()
    {
        Turn(); // ?i?u ch?nh h??ng
        Move(); // Ti?n v? ph�a m?c ti�u
    }

    void Turn()
    {
        Vector3 direction = target.position - transform.position; // T�nh h??ng t?i m?c ti�u
        if (direction.magnitude > 0f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction); // Xoay h??ng v? ph�a m?c ti�u
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime); // L�m m??t g�c quay
        }
    }

    void Move()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Ti?n g?n t?i m?c ti�u n?u c�n kho?ng c�ch
        if (distanceToTarget > 1f)
        {
            transform.position += transform.forward * move * Time.deltaTime;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // C?p nh?t thanh m�u theo t? l?
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u ??i t??ng va ch?m l� Bullet
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by Bullet! Destroying enemy.");
            Destroy(gameObject); // X�a k? ??ch
        }

        // Ki?m tra n?u ??i t??ng va ch?m l� Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Tr? 10 m�u c?a Player
                Debug.Log("Player hit by attack! Player health reduced by 10.");
            }
        }
    }
}
