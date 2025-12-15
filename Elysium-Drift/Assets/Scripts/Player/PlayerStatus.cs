using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Base Status")]
    public int level = 1;
    public float STR = 10f;

    [Header("HP")]
    public float maxHP = 100f;
    public float HP { get; private set; }

    [Header("MP")]
    public float maxMP = 50f;
    public float MP { get; private set; }

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina { get; private set; }

    void Awake()
    {
        HP = maxHP;
        MP = maxMP;
        stamina = maxStamina;
    }

    // ===== HP =====
    public void TakeDamage(float damage)
    {
        HP = Mathf.Max(HP - damage, 0f);
    }

    // ===== MP =====
    public bool ConsumeMP(float amount)
    {
        if (MP < amount) return false;
        MP -= amount;
        return true;
    }

    // ===== Stamina =====
    public bool CanDash()
    {
        return stamina > 0f;
    }

    public void ConsumeStamina()
    {
        ConsumeStamina(20f);
    }

    public void ConsumeStamina(float amountPerSecond)
    {
        stamina = Mathf.Max(stamina - amountPerSecond * Time.deltaTime, 0f);
    }

    public void RecoverStamina()
    {
        RecoverStamina(30f);
    }

    public void RecoverStamina(float amountPerSecond)
    {
        stamina = Mathf.Min(stamina + amountPerSecond * Time.deltaTime, maxStamina);
    }
}
