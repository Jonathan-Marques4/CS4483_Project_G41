using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("DRAG YOUR PLAYER HERE")]
    [SerializeField] private HealthComponent playerHealth;

    void Update()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = Mathf.RoundToInt(playerHealth.CurrentHealth) 
                           + " / " 
                           + Mathf.RoundToInt(playerHealth.MaxHealth);
        }
    }
}