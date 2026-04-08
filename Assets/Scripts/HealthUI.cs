using UnityEngine;
using TMPro;           // Use TextMeshPro (recommended)
using UnityEngine.UI;   // If you prefer legacy Text

public class HealthUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI healthText;     // Recommended
    // [SerializeField] private Text healthText;             // Legacy UI Text

    [SerializeField] private string format = "{0}/{1}";      // Example: "75/100"

    private HealthComponent healthComponent;

    void Start()
    {
        // Find the HealthComponent on the player
        healthComponent = FindObjectOfType<HealthComponent>(); // Or assign via inspector

        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found!");
            return;
        }

        // Subscribe to health events
        healthComponent.OnDamaged += UpdateHealthUI;
        healthComponent.OnHealed += UpdateHealthUI;
        healthComponent.OnDeath += OnPlayerDeath;

        // Initial update
        UpdateHealthUI(0);
    }

    private void UpdateHealthUI(float amount)
    {
        if (healthText != null)
        {
            healthText.text = string.Format(format, 
                Mathf.RoundToInt(healthComponent.CurrentHealth), 
                Mathf.RoundToInt(healthComponent.MaxHealth));
        }
    }

    private void OnPlayerDeath()
    {
        if (healthText != null)
        {
            healthText.text = "DEAD";
            // Optional: change color to red
            healthText.color = Color.red;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (healthComponent != null)
        {
            healthComponent.OnDamaged -= UpdateHealthUI;
            healthComponent.OnHealed -= UpdateHealthUI;
            healthComponent.OnDeath -= OnPlayerDeath;
        }
    }
}