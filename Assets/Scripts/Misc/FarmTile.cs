using UnityEngine;

public class FarmTile : MonoBehaviour{
    
    public bool canPlant = true;
    public bool isOccupied = false;

    public Transform cropSpawnPoint;
    public CropPlant currentCrop;

    public bool TryPlant(CropData cropData, GameObject cropPlantPrefab){

        if (!canPlant || isOccupied || cropData == null || cropPlantPrefab == null)
            return false;

        GameObject cropObj = Instantiate(cropPlantPrefab, cropSpawnPoint.position, Quaternion.identity);
        CropPlant cropPlant = cropObj.GetComponent<CropPlant>();

        if (cropPlant == null){

            Debug.LogError("Crop prefab is missing CropPlant component.");
            Destroy(cropObj);
            return false;
        }

        cropPlant.Initialize(cropData, this);

        currentCrop = cropPlant;
        isOccupied = true;
        return true;
    }

    public void ClearCrop(){

        currentCrop = null;
        isOccupied = false;
    }

    public void GrowCropOneDay(){

        if (currentCrop != null){

            currentCrop.GrowOneDay();
        }
    }

    public void TryHarvest(){

        if (currentCrop != null && currentCrop.CanHarvest()){

            currentCrop.Harvest();
        }
    }

    public void LoadCrop(CropData cropData, GameObject cropPlantPrefab, int growthStage, int daysGrown)
    {
        if (cropData == null || cropPlantPrefab == null || cropSpawnPoint == null) return;

        GameObject cropObj = Instantiate(cropPlantPrefab, cropSpawnPoint.position, Quaternion.identity);
        CropPlant cropPlant = cropObj.GetComponent<CropPlant>();

        if (cropPlant == null)
        {
            Destroy(cropObj);
            return;
        }

        cropPlant.Initialize(cropData, this);
        cropPlant.LoadGrowthState(growthStage, daysGrown);

        currentCrop = cropPlant;
        isOccupied = true;
    }
}