using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    private HealthComponent playerHealth;

    void Start()
    {
        if (PlayerStats.Instance != null)
            playerHealth = PlayerStats.Instance.Health;
    }

    void Update()
    {
        if (text != null && playerHealth != null)
            text.text = "HP: " + playerHealth.CurrentHealth + "/" + playerHealth.MaxHealth;
    }
}