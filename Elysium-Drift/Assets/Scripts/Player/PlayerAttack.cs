using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private SwordHitbox swordHitbox;

    private PlayerStatus status;
    private PlayerMagic magic;
    private bool canAttack = true;

    void Start()
    {
        status = GetComponent<PlayerStatus>();
        magic = GetComponent<PlayerMagic>();

        swordHitbox.SetOwner(this);
        swordHitbox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (magic != null && magic.IsCasting) return;

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;
        swordHitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        swordHitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    public float GetAttackPower()
    {
        return status.STR;
    }
}
