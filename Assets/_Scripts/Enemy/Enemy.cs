using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float move = 10f; // T?c ?? di chuy?n
    [SerializeField] private float rotationalDamp = 5f; // ?? tr? quay (t?ng ?? quay nhanh h?n)
    [SerializeField] private float chaseRange = 30f; // Ph?m vi ?u?i theo
    private Transform target; // ??i t??ng m?c ti�u
    [SerializeField] private int damage = 10; // S�t th??ng m?i l?n ch?m
    [SerializeField] private Image healthBar; // Thanh m�u hi?n th? UI
    private int maxHealth = 100; // M�u t?i ?a c?a nh�n v?t
    private int currentHealth; // M�u hi?n t?i c?a nh�n v?t

    void Start()
    {
        // T�m ??i t??ng v?i tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }

        // Kh?i t?o m�u cho nh�n v?t
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= chaseRange) // ?u?i theo trong ph?m vi ?� ??t
            {
                ChasePlayer();
            }
        }
    }

    void ChasePlayer()
    {
        Turn(); // ?i?u ch?nh h??ng v? ph�a ng??i ch?i
        Move(); // Ti?n v? ph�a tr??c
    }

    void Turn()
    {
        Vector3 direction = target.position - transform.position; // T�nh h??ng t?i ng??i ch?i

        // N?u kho?ng c�ch nh?, kh�ng c?n quay
        if (direction.magnitude > 0f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction); // Quay v? ph�a ng??i ch?i
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime); // ?i?u ch?nh g�c quay
        }
    }

    void Move()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // N?u qu� g?n m?c ti�u, gi?m t?c ?? ?? kh�ng di chuy?n qu� xa
        if (distanceToTarget > 1f)
        {
            transform.position += transform.forward * move * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u ??i t??ng va ch?m c� tag "Player"
        if (other.CompareTag("Player"))
        {
            TakeDamage(damage); // Tr? m�u c?a nh�n v?t
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage; // Gi?m m�u
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Gi?i h?n t? 0 ??n t?i ?a
        UpdateHealthBar(); // C?p nh?t thanh m�u
        if (currentHealth <= 0)
        {
            Debug.Log("Player defeated!");
            Destroy(target.gameObject); // X�a nh�n v?t khi h?t m�u
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // C?p nh?t thanh m�u theo t? l?
        }
    }
}
