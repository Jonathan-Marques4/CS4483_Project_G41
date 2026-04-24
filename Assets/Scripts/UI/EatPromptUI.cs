using TMPro;
using UnityEngine;

public class EatPromptUI : MonoBehaviour
{
    public static EatPromptUI Instance;

    public GameObject panel;
    public TextMeshProUGUI promptText;

    private ItemData pendingItem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        HidePrompt();
    }

    public void ShowPrompt(ItemData item)
    {
        if (item == null) return;

        pendingItem = item;

        if (panel != null)
            panel.SetActive(true);

        if (promptText != null)
        {
            string message = $"Do you want to eat {item.itemName}?";

            if (item.energyRestoreAmount > 0)
                message += $"\nRestores {item.energyRestoreAmount} energy.";

            if (item.healthRestoreAmount > 0)
                message += $"\nRestores {item.healthRestoreAmount} HP.";

            promptText.text = message;
        }

        if (TimeManager.Instance != null)
            TimeManager.Instance.SetTimePaused(true);
    }

    public void ConfirmEat()
    {
        if (pendingItem != null && InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ConsumeSelectedItemForEnergy();
        }

        HidePrompt();
    }

    public void CancelEat()
    {
        HidePrompt();
    }

    public void HidePrompt()
    {
        pendingItem = null;

        if (panel != null)
            panel.SetActive(false);

        if (TimeManager.Instance != null)
        {
            if (MenuManager.Instance == null || !MenuManager.Instance.IsMenuOpen())
            {
                TimeManager.Instance.SetTimePaused(false);
            }
        }
    }

    public bool IsPromptOpen()
    {
        return panel != null && panel.activeSelf;
    }
}