using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // -------- 基本ステータス --------
    public int level = 1;

    public float maxHP = 100f;
    public float currentHP = 100f;

    public float maxMP = 50f;
    public float currentMP = 50f;

    public float maxStamina = 100f;
    public float currentStamina = 100f;

    public int STR = 10;

    // -------- 魔法設定 --------
    public float magicPowerE = 30f;
    public float magicPowerQ = 50f;
    public float magicCostE = 10f;
    public float magicCostQ = 20f;

    // -------- ダッシュ用 --------
    public float staminaConsumeDash = 20f;
    public float staminaConsumeDashPerSec = 15f;
    public float staminaRecoverPerSec = 10f;

    // -------- マップ用情報 --------
    public Vector3 position;   // x,y,z
    public Vector3 forward;    // 向いている方向

    // -------- 汎用関数 --------
    public bool UseStamina(float value)
    {
        if (currentStamina < value) return false;
        currentStamina -= value;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        return true;
    }

    public bool UseMP(float value)
    {
        if (currentMP < value) return false;
        currentMP -= value;
        currentMP = Mathf.Clamp(currentMP, 0, maxMP);
        return true;
    }

    public void RecoverStamina()
    {
        currentStamina += staminaRecoverPerSec * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }
}
