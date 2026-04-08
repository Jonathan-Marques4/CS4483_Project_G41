using UnityEngine;

public class InventoryClickController : MonoBehaviour
{
    public InventorySlotUI[] allSlots;

    private int selectedSlotIndex = -1;

    public void OnSlotClicked(int clickedIndex)
    {
        // If chest is open, clicking any player slot (including top hotbar 0-6)
        // moves it into the chest
        if (ChestUI.Instance != null && ChestUI.Instance.IsChestOpen())
        {
            if (ChestTransferController.Instance != null)
            {
                ChestTransferController.Instance.OnPlayerInventorySlotClicked(clickedIndex);
            }
            return;
        }

        // Normal inventory move/swap logic
        if (InventoryManager.Instance == null) return;
        if (clickedIndex < 0 || clickedIndex >= InventoryManager.Instance.slots.Count) return;

        if (selectedSlotIndex == -1)
        {
            if (!InventoryManager.Instance.slots[clickedIndex].IsEmpty())
            {
                selectedSlotIndex = clickedIndex;
                RefreshClickHighlights();
            }
            return;
        }

        if (selectedSlotIndex == clickedIndex)
        {
            selectedSlotIndex = -1;
            RefreshClickHighlights();
            return;
        }

        InventoryManager.Instance.SwapSlots(selectedSlotIndex, clickedIndex);
        selectedSlotIndex = -1;
        RefreshClickHighlights();
    }

    public void RefreshClickHighlights()
    {
        if (allSlots == null || InventoryManager.Instance == null) return;

        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i] == null || allSlots[i].selectionHighlight == null) continue;

            bool moveSelected = (i == selectedSlotIndex);
            bool hotbarSelected = (i < InventoryManager.HotbarSize && i == InventoryManager.Instance.selectedHotbarIndex);

            allSlots[i].selectionHighlight.SetActive(moveSelected || hotbarSelected);
        }
    }
}