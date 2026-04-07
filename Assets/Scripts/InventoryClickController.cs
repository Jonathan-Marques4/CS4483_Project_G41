using UnityEngine;

public class InventoryClickController : MonoBehaviour{
    
    public InventorySlotUI[] allSlots;

    private int selectedSlotIndex = -1;

    public void OnSlotClicked(int clickedIndex){

        if (InventoryManager.Instance == null) return;
        if (clickedIndex < 0 || clickedIndex >= InventoryManager.Instance.slots.Count) return;

        // First click: pick a slot
        if (selectedSlotIndex == -1){

            if (!InventoryManager.Instance.slots[clickedIndex].IsEmpty()){

                selectedSlotIndex = clickedIndex;
                RefreshClickHighlights();
            }
            return;
        }

        // Clicking same slot again cancels selection
        if (selectedSlotIndex == clickedIndex){
            selectedSlotIndex = -1;
            RefreshClickHighlights();
            return;
        }

        // Second click: swap/move
        InventoryManager.Instance.SwapSlots(selectedSlotIndex, clickedIndex);

        selectedSlotIndex = -1;
        RefreshClickHighlights();
    }

    public void RefreshClickHighlights(){
        if (allSlots == null) return;

        for (int i = 0; i < allSlots.Length; i++){

            bool isSelectedForMove = (i == selectedSlotIndex);

            // optional: add separate click highlight later, add it here
            if (allSlots[i].selectionHighlight != null){
                bool hotbarSelected = InventoryManager.Instance != null &&
                                      i == InventoryManager.Instance.selectedHotbarIndex &&
                                      i < InventoryManager.HotbarSize;

                allSlots[i].selectionHighlight.SetActive(isSelectedForMove || hotbarSelected);
            }
        }
    }
}