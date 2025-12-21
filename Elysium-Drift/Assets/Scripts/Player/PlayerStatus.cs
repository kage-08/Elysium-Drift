using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Base")]
    public int level = 1;
    public int STR = 10;

    [Header("HP")]
    public float maxHP = 100f;
    public float HP;

    [Header("MP")]
    public float maxMP = 50f;
    public float MP;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina;

    void Awake()
    {
        HP = maxHP;
        MP = maxMP;
        stamina = maxStamina;
    }
}
