using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float maxHP = 50f;
    public float STR = 10f;
    public float HP { get; private set; }

    void Awake()
    {
        HP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0f)
            Destroy(gameObject);
    }
}
