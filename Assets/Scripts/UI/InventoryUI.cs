using UnityEngine;

public class InventoryUI : MonoBehaviour{

    
    public InventorySlotUI[] allSlots;
    public InventoryClickController clickController;

    private void Start(){

        if (InventoryManager.Instance != null){

            InventoryManager.Instance.OnInventoryChanged += RefreshInventory;
            InventoryManager.Instance.OnSelectedHotbarChanged += OnHotbarSelectionChanged;
        }

        for (int i = 0; i < allSlots.Length; i++){

            allSlots[i].slotIndex = i;
            allSlots[i].clickController = clickController;
        }

        RefreshInventory();
    }

    private void OnDestroy(){

        if (InventoryManager.Instance != null){

            InventoryManager.Instance.OnInventoryChanged -= RefreshInventory;
            InventoryManager.Instance.OnSelectedHotbarChanged -= OnHotbarSelectionChanged;
        }
    }

    private void OnHotbarSelectionChanged(int index){

        RefreshInventory();
    }

    public void RefreshInventory(){

        if (InventoryManager.Instance == null || allSlots == null) return;

        for (int i = 0; i < allSlots.Length; i++){

            InventorySlot slot = InventoryManager.Instance.slots[i];
            bool selected = i == InventoryManager.Instance.selectedHotbarIndex;
            allSlots[i].SetSlot(slot, selected);
        }

        if (clickController != null){

            clickController.RefreshClickHighlights();
        }
    }
}