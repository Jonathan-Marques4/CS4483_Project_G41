using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public Slider xpSlider;

    private void Start()
    {
        if (PlayerLevelSystem.Instance != null)
        {
            PlayerLevelSystem.Instance.OnXPChanged += UpdateUI;
            PlayerLevelSystem.Instance.OnLevelChanged += UpdateUI;
        }

        UpdateUI();
    }

    private void OnDestroy()
    {
        if (PlayerLevelSystem.Instance != null)
        {
            PlayerLevelSystem.Instance.OnXPChanged -= UpdateUI;
            PlayerLevelSystem.Instance.OnLevelChanged -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        if (PlayerLevelSystem.Instance == null) return;

        if (levelText != null)
            levelText.text = "Level: " + PlayerLevelSystem.Instance.currentLevel;

        if (xpText != null)
            xpText.text = "XP: " + PlayerLevelSystem.Instance.currentXP + "/" + PlayerLevelSystem.Instance.xpToNextLevel;

        if (xpSlider != null)
        {
            xpSlider.maxValue = PlayerLevelSystem.Instance.xpToNextLevel;
            xpSlider.value = PlayerLevelSystem.Instance.currentXP;
        }
    }
}