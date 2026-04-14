using System.Collections.Generic;

[System.Serializable]
public class ChestItemSaveData
{
    public string itemName;
    public int quantity;
}

[System.Serializable]
public class ChestSaveData
{
    public List<ChestItemSaveData> items = new List<ChestItemSaveData>();
}