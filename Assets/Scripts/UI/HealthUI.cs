using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    private HealthComponent playerHealth;

    void Update()
    {
        // Grab the reference once the player is available
        if (playerHealth == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<HealthComponent>();
            return;
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.MaxHealth;
            healthSlider.value    = playerHealth.CurrentHealth;
        }

        if (healthText != null)
            healthText.text = Mathf.RoundToInt(playerHealth.CurrentHealth)
                            + " / "
                            + Mathf.RoundToInt(playerHealth.MaxHealth);
    }
}
