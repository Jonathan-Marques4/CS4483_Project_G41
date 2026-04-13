using UnityEngine;

public class ChestTransferController : MonoBehaviour
{
    public static ChestTransferController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OnPlayerInventorySlotClicked(int slotIndex)
    {
        if (InventoryManager.Instance == null || ChestUI.Instance == null) return;
        if (!ChestUI.Instance.IsChestOpen()) return;

        ChestInteraction chest = ChestUI.Instance.GetCurrentChest();
        if (chest == null || chest.chestInventory == null) return;
        if (slotIndex < 0 || slotIndex >= InventoryManager.Instance.slots.Count) return;

        InventorySlot playerSlot = InventoryManager.Instance.slots[slotIndex];
        if (playerSlot == null || playerSlot.IsEmpty()) return;

        ItemData item = playerSlot.item;
        int quantity = playerSlot.quantity;

        bool added = chest.chestInventory.AddItem(item, quantity);

        if (added)
        {
            playerSlot.Clear();

            InventoryManager.Instance.ForceRefresh();
            ChestUI.Instance.RefreshChestUI();

            Debug.Log("Moved item from player inventory/hotbar to chest: " + item.itemName);
        }
        else
        {
            Debug.Log("Could not move item into chest.");
        }
    }

    public void OnChestSlotClicked(int slotIndex)
    {
        if (InventoryManager.Instance == null || ChestUI.Instance == null) return;
        if (!ChestUI.Instance.IsChestOpen()) return;

        ChestInteraction chest = ChestUI.Instance.GetCurrentChest();
        if (chest == null || chest.chestInventory == null) return;
        if (slotIndex < 0 || slotIndex >= chest.chestInventory.slots.Count) return;

        InventorySlot chestSlot = chest.chestInventory.slots[slotIndex];
        if (chestSlot == null || chestSlot.IsEmpty()) return;

        ItemData item = chestSlot.item;
        int quantity = chestSlot.quantity;

        bool added = InventoryManager.Instance.AddItem(item, quantity);

        if (added)
        {
            chestSlot.Clear();

            InventoryManager.Instance.ForceRefresh();
            ChestUI.Instance.RefreshChestUI();

            Debug.Log("Moved item from chest to player inventory: " + item.itemName);
        }
        else
        {
            Debug.Log("Could not move item into player inventory.");
        }
    }
}