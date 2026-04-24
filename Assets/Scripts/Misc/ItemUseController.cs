using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    public PlayerPlanting playerPlanting;
    public Camera mainCamera;

    private void Update()
    {
        if (InventoryManager.Instance == null) return;
        if (GameStateManager.Instance != null && GameStateManager.Instance.IsGameplayBlocked()) return;

        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }
    }

    private void HandleLeftClick()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned on ItemUseController.");
            return;
        }

        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);



        // 1. Bed click
        if (hit.collider != null)
        {
            BedInteraction bed = hit.collider.GetComponent<BedInteraction>();
            if (bed != null)
            {
                if (bed.IsPlayerInRange(transform))
                {
                    if (SleepPromptUI.Instance != null)
                    {
                        SleepPromptUI.Instance.ShowPrompt(bed);
                    }
                }
                else
                {
                    Debug.Log("Too far away from bed.");
                }

                return;
            }
        }

        // Chest click
        if (hit.collider != null)
        {
            ChestInteraction chest = hit.collider.GetComponent<ChestInteraction>();
            if (chest != null)
            {
                if (chest.IsPlayerInRange(transform))
                {
                    chest.OpenChest();
                }
                else
                {
                    Debug.Log("Too far away from chest.");
                }
                return;
            }
        }

        // 2. Harvest clicked crop
        if (playerPlanting != null && playerPlanting.TryHarvestClickedCrop())
        {
            return;
        }

        // 3. Selected hotbar item
        InventorySlot selectedSlot = InventoryManager.Instance.GetSelectedHotbarSlot();

        if (selectedSlot == null || selectedSlot.IsEmpty() || selectedSlot.item == null)
        {
            return;
        }

        ItemData selectedItem = selectedSlot.item;

        // 4. Food
        if (selectedItem.energyRestoreAmount > 0)
        {
            if (EatPromptUI.Instance != null)
            {
                EatPromptUI.Instance.ShowPrompt(selectedItem);
            }
            return;
        }

        // 5. Seeds
        if (selectedItem.isPlantable)
        {
            if (playerPlanting != null)
            {
                playerPlanting.TryPlantSelectedSeed();
            }
            return;
        }

        // Weapons are handled by PlayerCombat directly — nothing to do here
        if (selectedItem.itemType != ItemType.Weapon)
            Debug.Log("Selected item has no left-click use yet.");
    }
}