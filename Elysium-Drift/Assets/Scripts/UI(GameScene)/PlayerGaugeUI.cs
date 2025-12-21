using UnityEngine;
using UnityEngine.UI;

public class PlayerGaugeUI : MonoBehaviour
{
    public PlayerStatus status;

    public Image hpFront;
    public Image hpBack;
    public Image mpBar;
    public Image staminaBar;

    void Update()
    {
        UpdateHP();
        UpdateMP();
        UpdateStamina();
    }

    void UpdateHP()
    {
        float ratio = status.HP / status.maxHP;
        hpFront.fillAmount = ratio;
        hpBack.fillAmount = Mathf.Lerp(hpBack.fillAmount, ratio, Time.deltaTime * 2f);

        if (ratio <= 0.1f) hpFront.color = Color.red;
        else if (ratio <= 0.3f) hpFront.color = new Color(1f, 0.5f, 0f);
    }

    void UpdateMP()
    {
        mpBar.fillAmount = status.MP / status.maxMP;
    }

    void UpdateStamina()
    {
        staminaBar.fillAmount = status.stamina / status.maxStamina;
    }
}
