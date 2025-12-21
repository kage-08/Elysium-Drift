using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatus status;
    public float swordDamageMultiplier = 1f;

    [Header("Magic")]
    public float magicDamage = 30f;
    public float magicCost = 10f;
    public float castTime = 1.5f;

    bool isCasting;

    void Update()
    {
        SwordAttack();
        MagicAttack(KeyCode.Q);
        MagicAttack(KeyCode.E);
    }

    void SwordAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isCasting)
        {
            // Œ•‚ÌCollider‚Å”»’èi‚±‚±‚Å‚Íˆ—‚¾‚¯j
        }
    }

    void MagicAttack(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            if (isCasting)
            {
                StopAllCoroutines();
                isCasting = false;
                return;
            }

            if (status.MP >= magicCost)
                StartCoroutine(CastMagic());
        }
    }

    IEnumerator CastMagic()
    {
        isCasting = true;
        yield return new WaitForSeconds(castTime);

        status.MP -= magicCost;
        isCasting = false;
    }
}
