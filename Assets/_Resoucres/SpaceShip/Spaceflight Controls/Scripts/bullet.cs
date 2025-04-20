using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour
{
    public PlayerRef shooter;
    public int damage = 10;
    public GameObject explo;

    private bool spawnedReady = false;

    private TickTimer despawnTimer;

    public override void Spawned()
    {
        spawnedReady = true;
        if (Object.HasStateAuthority)
            despawnTimer = TickTimer.CreateFromSeconds(Runner, 3f);

        Debug.Log("Bullet Object: " + Object);
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority && despawnTimer.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!spawnedReady || !Object || !Object.HasStateAuthority) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var health = other.gameObject.GetComponent<PlayerHP>();
            if (health != null && health.Object.InputAuthority != shooter)
            {
                health.RpcTakeDamage(damage);
            }
        }

        // Spawn hiệu ứng nổ (chỉ nếu bạn muốn sync)
        Runner.Spawn(
            explo,
            other.contacts[0].point,
            Quaternion.identity,
            Object.InputAuthority, // có thể thay bằng shooter hoặc player authority
            (runner, obj) =>
            {
                var explotion = obj.GetComponent<PSDestroy>();
                if (explotion != null) explotion.runner = runner;
            });

        Runner.Despawn(Object);
    }
}