using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance;

    private Dictionary<string, ChestSaveData> chestStates = new Dictionary<string, ChestSaveData>();
    private Dictionary<string, FarmTileSaveData> farmTileStates = new Dictionary<string, FarmTileSaveData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveChestState(string objectID, ChestSaveData data)
    {
        chestStates[objectID] = data;
    }

    public bool TryGetChestState(string objectID, out ChestSaveData data)
    {
        return chestStates.TryGetValue(objectID, out data);
    }

    public void SaveFarmTileState(string objectID, FarmTileSaveData data)
    {
        farmTileStates[objectID] = data;
    }

    public bool TryGetFarmTileState(string objectID, out FarmTileSaveData data)
    {
        return farmTileStates.TryGetValue(objectID, out data);
    }


    public void AdvanceAllSavedCropsOneDay()
    {
        if (CropDatabase.Instance == null) return;

        List<string> keys = new List<string>(farmTileStates.Keys);

        foreach (string key in keys)
        {
            FarmTileSaveData data = farmTileStates[key];

            if (!data.isOccupied || string.IsNullOrEmpty(data.cropID))
                continue;

            CropData cropData = CropDatabase.Instance.GetCropByID(data.cropID);
            if (cropData == null)
                continue;

            data.daysGrown++;

            int totalDays = Mathf.Max(1, cropData.totalGrowthDays);

            if (data.daysGrown >= totalDays)
            {
                data.growthStage = 2;
            }
            else if (data.daysGrown >= Mathf.CeilToInt(totalDays / 2f))
            {
                data.growthStage = 1;
            }
            else
            {
                data.growthStage = 0;
            }

            farmTileStates[key] = data;
        }
    }
}