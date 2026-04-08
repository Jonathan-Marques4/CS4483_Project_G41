using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject cropPlantPrefab;
    public int plantingEnergyCost = 2;
    public float plantingRange = 2f;

    public bool TryPlantSelectedSeed()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera reference is missing on PlayerPlanting.");
            return false;
        }

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager.Instance is null.");
            return false;
        }

        InventorySlot selectedSlot = InventoryManager.Instance.GetSelectedHotbarSlot();

        if (selectedSlot == null || selectedSlot.IsEmpty())
        {
            Debug.Log("Selected hotbar slot is empty.");
            return false;
        }

        ItemData selectedItem = selectedSlot.item;

        if (selectedItem == null || !selectedItem.isPlantable || selectedItem.cropToPlant == null)
        {
            Debug.Log("Selected item is not a plantable seed.");
            return false;
        }

        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider == null)
        {
            Debug.Log("Nothing was clicked.");
            return false;
        }

        FarmTile farmTile = hit.collider.GetComponent<FarmTile>();

        if (farmTile == null)
        {
            Debug.Log("Clicked object is not a FarmTile.");
            return false;
        }

        float distanceToTile = Vector2.Distance(transform.position, farmTile.transform.position);

        if (distanceToTile > plantingRange)
        {
            Debug.Log("Too far away to plant.");
            return false;
        }

        if (PlayerEnergy.Instance != null)
        {
            bool hadEnoughEnergy = PlayerEnergy.Instance.UseEnergy(plantingEnergyCost);

            if (!hadEnoughEnergy)
            {
                Debug.Log("Not enough energy to plant.");
                return false;
            }
        }

        bool planted = farmTile.TryPlant(selectedItem.cropToPlant, cropPlantPrefab);

        if (planted)
        {
            selectedSlot.quantity--;

            if (selectedSlot.quantity <= 0)
            {
                selectedSlot.Clear();
            }

            InventoryManager.Instance.ForceRefresh();
            Debug.Log("Planted " + selectedItem.itemName);
            return true;
        }

        return false;
    }

    public bool TryHarvestClickedCrop()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera reference is missing on PlayerPlanting.");
            return false;
        }

        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider == null)
            return false;

        FarmTile farmTile = hit.collider.GetComponent<FarmTile>();

        if (farmTile == null)
            return false;

        float distanceToTile = Vector2.Distance(transform.position, farmTile.transform.position);

        if (distanceToTile > plantingRange)
        {
            Debug.Log("Too far away to harvest.");
            return false;
        }

        if (farmTile.currentCrop != null && farmTile.currentCrop.CanHarvest())
        {
            farmTile.TryHarvest();
            return true;
        }

        return false;
    }
}