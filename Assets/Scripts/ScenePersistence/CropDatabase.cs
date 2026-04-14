using System.Collections.Generic;
using UnityEngine;

public class CropDatabase : MonoBehaviour
{
    public static CropDatabase Instance;

    public List<CropData> allCrops = new List<CropData>();

    private Dictionary<string, CropData> cropLookup = new Dictionary<string, CropData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildLookup();
    }

    private void BuildLookup()
    {
        cropLookup.Clear();

        foreach (CropData crop in allCrops)
        {
            if (crop != null && !cropLookup.ContainsKey(crop.cropID))
            {
                cropLookup.Add(crop.cropID, crop);
            }
        }
    }

    public CropData GetCropByID(string cropID)
    {
        if (cropLookup.TryGetValue(cropID, out CropData crop))
            return crop;

        return null;
    }
}