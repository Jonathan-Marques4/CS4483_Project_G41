using UnityEngine;

public class CropPlant : MonoBehaviour{
    
    public CropData cropData;
    public FarmTile plantedTile;

    private SpriteRenderer spriteRenderer;

    private int currentStage = 0;
    private int daysGrown = 0;
    private bool isFullyGrown = false;

    private void Awake(){

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(CropData data, FarmTile tile){

        cropData = data;
        plantedTile = tile;
        currentStage = 0;
        daysGrown = 0;
        isFullyGrown = false;

        UpdateVisual();
    }

    public void GrowOneDay(){

        if (cropData == null || isFullyGrown) return;

        daysGrown++;

        int totalDays = Mathf.Max(1, cropData.totalGrowthDays);

        if (daysGrown >= totalDays){

            currentStage = 2;
            isFullyGrown = true;
        }
        else if (daysGrown >= Mathf.CeilToInt(totalDays / 2f)){

            currentStage = 1;
        }
        else{

            currentStage = 0;
        }

        UpdateVisual();
    }

    private void UpdateVisual(){

        if (spriteRenderer == null || cropData == null) return;

        switch (currentStage){

            case 0:
                spriteRenderer.sprite = cropData.stage0SeedSprite;
                break;

            case 1:
                spriteRenderer.sprite = cropData.stage1MidSprite;
                break;

            case 2:
                spriteRenderer.sprite = cropData.stage2FullSprite;
                break;
        }
    }

    public bool CanHarvest(){

        return isFullyGrown;
    }

    public void Harvest()
    {
        if (!CanHarvest() || cropData == null) return;

        if (InventoryManager.Instance != null){

            InventoryManager.Instance.AddItem(cropData.harvestItem, cropData.harvestAmount);
        }

        if (plantedTile != null){
            
            plantedTile.ClearCrop();
        }

        Destroy(gameObject);
    }
}