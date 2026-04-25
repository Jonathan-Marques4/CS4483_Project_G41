using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float armor = 0f;

    public float MaxHealth => maxHealth;
    public float CurrentHealth { get; private set; }
    public float Armor { get => armor; set => armor = Mathf.Max(0f, value); }
    public bool IsAlive => CurrentHealth > 0f;

    public event Action<float> OnDamaged;
    public event Action<float> OnHealed;
    public event Action OnDeath;

    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        float actual = Mathf.Max(1f, amount - armor);
        CurrentHealth = Mathf.Max(0f, CurrentHealth - actual);

        OnDamaged?.Invoke(actual);

        if (CurrentHealth <= 0f)
            OnDeath?.Invoke();
    }

    public void Heal(float amount)
    {
        if (!IsAlive) return;
        float actual = Mathf.Min(amount, maxHealth - CurrentHealth);
        CurrentHealth += actual;
        OnHealed?.Invoke(actual);
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void ReviveFull()
    {
        CurrentHealth = maxHealth;
        OnHealed?.Invoke(maxHealth);
    }

    // ==================== TEST METHODS ====================
    // Remove these later if you want
    public void TestDamage()
    {
        TakeDamage(25f);
    }

    public void TestHeal()
    {
        Heal(25f);
    }
}