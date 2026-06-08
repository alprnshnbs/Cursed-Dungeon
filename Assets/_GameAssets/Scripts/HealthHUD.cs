using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [Header("UI Referansları")]
    public Slider healthSlider;

    // HealthSystem tarafından can her değiştiğinde bu fonksiyon otomatik çağrılacaktır
    public void UpdateHUD(int currentHealth, int maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
}