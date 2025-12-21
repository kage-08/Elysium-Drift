using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public EnemyStatus status;
    public Transform player;
    public float attackRange = 2f;

    void Update()
    {
        if (status.HP <= 0)
            Die();
    }

    void Die()
    {
        ScoreManager.Instance.AddScore(status.rank);
        KillCountManager.Instance.AddKill();
        Destroy(gameObject);
    }
}
