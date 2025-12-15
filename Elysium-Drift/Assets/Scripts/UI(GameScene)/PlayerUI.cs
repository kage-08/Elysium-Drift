using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private PlayerStatus status;
    [SerializeField] private PlayerMagic magic;

    [Header("HP")]
    [SerializeField] private Image hpFront;
    [SerializeField] private Image hpBack;
    [SerializeField] private float hpBackDelay = 0.5f;
    [SerializeField] private float hpBackSpeed = 1.5f;

    [Header("MP")]
    [SerializeField] private Image mpFront;
    [SerializeField] private float mpSmoothSpeed = 5f;
    [SerializeField] private float mpBlinkInterval = 0.1f;

    [Header("Stamina")]
    [SerializeField] private Image staminaFront;
    [SerializeField] private float staminaSmoothSpeed = 5f;

    private Coroutine hpBackRoutine;
    private Coroutine mpBlinkRoutine;

    void Update()
    {
        UpdateHP();
        UpdateMP();
        UpdateStamina();
    }

    // =====================
    // HP（二重バー）
    // =====================
    private void UpdateHP()
    {
        float hpRate = status.HP / status.maxHP;

        // 表バーは即反映
        hpFront.fillAmount = hpRate;

        // 色変化
        if (hpRate <= 0.1f)
            hpFront.color = Color.red;
        else if (hpRate <= 0.3f)
            hpFront.color = new Color(1f, 0.5f, 0f); // オレンジ
        else
            hpFront.color = new Color(0.5f, 1f, 0.5f); // 黄緑

        // 裏バー（遅れて減る）
        if (hpBack.fillAmount > hpRate)
        {
            if (hpBackRoutine == null)
                hpBackRoutine = StartCoroutine(HpBackDelay(hpRate));
        }
        else
        {
            hpBack.fillAmount = hpRate;
        }
    }

    private IEnumerator HpBackDelay(float target)
    {
        yield return new WaitForSeconds(hpBackDelay);

        while (hpBack.fillAmount > target)
        {
            hpBack.fillAmount = Mathf.MoveTowards(
                hpBack.fillAmount,
                target,
                Time.deltaTime * hpBackSpeed
            );
            yield return null;
        }

        hpBackRoutine = null;
    }

    // =====================
    // MP（点滅＋滑らか）
    // =====================
    private void UpdateMP()
    {
        float target = status.MP / status.maxMP;

        mpFront.fillAmount = Mathf.Lerp(
            mpFront.fillAmount,
            target,
            Time.deltaTime * mpSmoothSpeed
        );

        if (magic != null && magic.IsCasting)
        {
            if (mpBlinkRoutine == null)
                mpBlinkRoutine = StartCoroutine(MPBlink());
        }
        else
        {
            if (mpBlinkRoutine != null)
            {
                StopCoroutine(mpBlinkRoutine);
                mpBlinkRoutine = null;
                mpFront.enabled = true;
            }
        }
    }

    private IEnumerator MPBlink()
    {
        while (true)
        {
            mpFront.enabled = !mpFront.enabled;
            yield return new WaitForSeconds(mpBlinkInterval);
        }
    }

    // =====================
    // スタミナ（滑らか）
    // =====================
    private void UpdateStamina()
    {
        float target = status.stamina / status.maxStamina;

        staminaFront.fillAmount = Mathf.Lerp(
            staminaFront.fillAmount,
            target,
            Time.deltaTime * staminaSmoothSpeed
        );
    }
}
