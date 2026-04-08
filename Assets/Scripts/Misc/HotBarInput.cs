using UnityEngine;

public class HotbarInput : MonoBehaviour
{
    void Update()
    {
        if (InventoryManager.Instance == null) return;
        if (GameStateManager.Instance != null && GameStateManager.Instance.IsGameplayBlocked()) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) InventoryManager.Instance.SelectHotbarSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) InventoryManager.Instance.SelectHotbarSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) InventoryManager.Instance.SelectHotbarSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) InventoryManager.Instance.SelectHotbarSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) InventoryManager.Instance.SelectHotbarSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) InventoryManager.Instance.SelectHotbarSlot(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) InventoryManager.Instance.SelectHotbarSlot(6);

        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0f)
            InventoryManager.Instance.CycleHotbar(-1);
        else if (scroll < 0f)
            InventoryManager.Instance.CycleHotbar(1);
    }
}