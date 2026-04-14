using UnityEngine;

public class FarmTileStateHandler : MonoBehaviour
{
    public GameObject cropPlantPrefab;

    private PersistentObjectID objectID;
    private FarmTile farmTile;

    private void Awake()
    {
        objectID = GetComponent<PersistentObjectID>();
        farmTile = GetComponent<FarmTile>();
    }

    private void Start()
    {
        LoadState();
    }

public void SaveState()
    {
        if (objectID == null || farmTile == null || SceneStateManager.Instance == null)
        {
            Debug.LogWarning("FarmTile SaveState failed due to missing references on " + gameObject.name);
            return;
        }

        FarmTileSaveData data = new FarmTileSaveData();
        data.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        data.isOccupied = farmTile.isOccupied;

        if (farmTile.currentCrop != null && farmTile.currentCrop.cropData != null)
        {
            data.cropID = farmTile.currentCrop.cropData.cropID;
            data.growthStage = farmTile.currentCrop.GetCurrentStage();
            data.daysGrown = farmTile.currentCrop.GetDaysGrown();

            Debug.Log($"Saving farm tile {objectID.objectID}: cropID={data.cropID}, stage={data.growthStage}, days={data.daysGrown}");
        }
        else
        {
            Debug.Log($"Saving farm tile {objectID.objectID}: empty");
        }

        SceneStateManager.Instance.SaveFarmTileState(objectID.objectID, data);
    }

    public void LoadState()
    {
        if (objectID == null || farmTile == null || SceneStateManager.Instance == null || CropDatabase.Instance == null)
        {
            Debug.LogWarning("FarmTile LoadState failed due to missing references on " + gameObject.name);
            return;
        }

        if (SceneStateManager.Instance.TryGetFarmTileState(objectID.objectID, out FarmTileSaveData data))
        {
            Debug.Log($"Loading farm tile {objectID.objectID}: occupied={data.isOccupied}, cropID={data.cropID}, stage={data.growthStage}, days={data.daysGrown}");

            if (data.isOccupied && !string.IsNullOrEmpty(data.cropID))
            {
                CropData cropData = CropDatabase.Instance.GetCropByID(data.cropID);

                if (cropData != null)
                {
                    farmTile.LoadCrop(cropData, cropPlantPrefab, data.growthStage, data.daysGrown);
                    Debug.Log($"Restored crop on tile {objectID.objectID}");
                }
                else
                {
                    Debug.LogWarning("Could not find CropData for cropID: " + data.cropID);
                }
            }
        }
        else
        {
            Debug.Log($"No saved farm tile state found for {objectID.objectID}");
        }
    }

}