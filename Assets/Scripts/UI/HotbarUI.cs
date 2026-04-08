using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public InventorySlotUI[] hotbarSlots;
    public InventoryClickController clickController;

    private void Start()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged += RefreshHotbar;
            InventoryManager.Instance.OnSelectedHotbarChanged += HandleHotbarSelectionChanged;
        }

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i] != null)
            {
                hotbarSlots[i].slotIndex = i; // top hotbar is player slots 0-6
                hotbarSlots[i].clickController = clickController;
            }
        }

        RefreshHotbar();
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryChanged -= RefreshHotbar;
            InventoryManager.Instance.OnSelectedHotbarChanged -= HandleHotbarSelectionChanged;
        }
    }

    private void HandleHotbarSelectionChanged(int index)
    {
        RefreshHotbar();
    }

    public void RefreshHotbar()
    {
        if (InventoryManager.Instance == null || hotbarSlots == null) return;

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i] == null) continue;

            InventorySlot slot = InventoryManager.Instance.GetHotbarSlot(i);
            bool selected = (i == InventoryManager.Instance.selectedHotbarIndex);

            hotbarSlots[i].SetSlot(slot, selected);
        }
    }
}