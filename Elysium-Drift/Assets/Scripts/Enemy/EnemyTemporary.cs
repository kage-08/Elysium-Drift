using UnityEngine;

public class EnemyTemporary : MonoBehaviour
{
    public float hp = 100f;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            PlayerAttack atk = other.GetComponentInParent<PlayerAttack>();
            if (atk != null)
            {
                TakeDamage(atk.GetPhysicalDamage());
            }
        }
    }
}
