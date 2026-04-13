using UnityEngine;

public class ChestUI : MonoBehaviour
{
    public static ChestUI Instance;

    public GameObject panel;
    public ChestSlotUI[] chestSlotUIs;

    private ChestInteraction currentChest;

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
        CloseChest();
    }

    public void OpenChest(ChestInteraction chest)
    {
        if (chest == null || chest.chestInventory == null) return;

        currentChest = chest;

        if (panel != null)
            panel.SetActive(true);

        RefreshChestUI();

        if (TimeManager.Instance != null)
            TimeManager.Instance.SetTimePaused(true);
    }

    public void CloseChest()
    {
        currentChest = null;

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

    public void RefreshChestUI()
    {
        if (chestSlotUIs == null) return;

        for (int i = 0; i < chestSlotUIs.Length; i++)
        {
            if (chestSlotUIs[i] == null) continue;

            chestSlotUIs[i].slotIndex = i;

            if (currentChest != null &&
                currentChest.chestInventory != null &&
                i < currentChest.chestInventory.slots.Count)
            {
                chestSlotUIs[i].SetSlot(currentChest.chestInventory.slots[i]);
            }
            else
            {
                chestSlotUIs[i].SetSlot(null);
            }
        }
    }

    public bool IsChestOpen()
    {
        return panel != null && panel.activeSelf;
    }

    public ChestInteraction GetCurrentChest()
    {
        return currentChest;
    }
}