using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Can Ayarları")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Olaylar (Events)")]
    // HUD'u güncellemek için kullanılır
    public UnityEvent<int, int> OnHealthChanged;
    // Karakter öldüğünde tetiklenir
    public UnityEvent OnDied;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Canın 0'ın altına düşmesini veya maksimumdan fazla olmasını engeller
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Can değiştiğinde HUD'a sinyal gönderir
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDied?.Invoke();

        // Karakteri sahneden gizler (İleride ölüm animasyonu veya Game Over ekranı eklenebilir)
        gameObject.SetActive(false);
    }
}