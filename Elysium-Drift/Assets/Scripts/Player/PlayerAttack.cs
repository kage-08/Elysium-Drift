using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatus status;
    public Collider swordCollider;
    public Camera playerCamera;

    private bool isAttacking = false;

    void Update()
    {
        // åïçUåÇ
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwordAttack());
        }

        // ñÇñ@ E
        if (Input.GetKeyDown(KeyCode.E))
        {
            MagicAttack(status.magicPowerE, status.magicCostE);
        }

        // ñÇñ@ Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MagicAttack(status.magicPowerQ, status.magicCostQ);
        }
    }

    IEnumerator SwordAttack()
    {
        if (isAttacking) yield break;

        isAttacking = true;
        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        swordCollider.enabled = false;
        isAttacking = false;
    }

    void MagicAttack(float power, float cost)
    {
        if (!status.UseMP(cost)) return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.TryGetComponent<EnemyTemporary>(out EnemyTemporary enemy))
            {
                enemy.TakeDamage(power);
            }
        }
    }

    public float GetPhysicalDamage()
    {
        return status.STR;
    }
}
