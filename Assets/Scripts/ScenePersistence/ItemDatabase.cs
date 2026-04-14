using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<ItemData> allItems = new List<ItemData>();

    private Dictionary<string, ItemData> itemLookup = new Dictionary<string, ItemData>();

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
        itemLookup.Clear();

        foreach (ItemData item in allItems)
        {
            if (item != null && !itemLookup.ContainsKey(item.itemName))
            {
                itemLookup.Add(item.itemName, item);
            }
        }
    }

    public ItemData GetItemByName(string itemName)
    {
        if (itemLookup.TryGetValue(itemName, out ItemData item))
            return item;

        return null;
    }
}