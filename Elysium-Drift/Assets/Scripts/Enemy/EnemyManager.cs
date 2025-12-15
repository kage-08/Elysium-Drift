using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.2f;

    private Transform player;
    private Rigidbody rb;
    private EnemyStatus status;
    private float attackTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        status = GetComponent<EnemyStatus>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
    }

    void FixedUpdate()
    {
        if (!player) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > chaseRange)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        if (dist > attackRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            dir.y = 0f;

            rb.linearVelocity = new Vector3(
                dir.x * moveSpeed,
                rb.linearVelocity.y,
                dir.z * moveSpeed
            );

            transform.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            Attack();
        }
    }

    private void Attack()
    {
        attackTimer -= Time.fixedDeltaTime;
        if (attackTimer > 0f) return;

        attackTimer = attackCooldown;
        player.GetComponent<PlayerStatus>()?.TakeDamage(status.STR);
    }
}
