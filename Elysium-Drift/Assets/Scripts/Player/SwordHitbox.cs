using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private PlayerAttack owner;

    public void SetOwner(PlayerAttack attack)
    {
        owner = attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyStatus enemy = other.GetComponent<EnemyStatus>();
        if (enemy != null)
        {
            enemy.TakeDamage(owner.GetAttackPower());
        }
    }
}
