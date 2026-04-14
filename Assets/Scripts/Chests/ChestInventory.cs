using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{
    public int chestSize = 14;
    public List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()
    {
        InitializeChest();
    }

    private void InitializeChest()
    {
        if (slots.Count == 0)
        {
            for (int i = 0; i < chestSize; i++)
            {
                slots.Add(new InventorySlot());
            }
        }
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0) return false;

        if (item.stackable)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].item == item && slots[i].quantity < item.maxStack)
                {
                    int spaceLeft = item.maxStack - slots[i].quantity;
                    int amountToAdd = Mathf.Min(spaceLeft, amount);

                    slots[i].quantity += amountToAdd;
                    amount -= amountToAdd;

                    if (amount <= 0)
                        return true;
                }
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsEmpty())
            {
                int amountToPlace = item.stackable ? Mathf.Min(item.maxStack, amount) : 1;

                slots[i].item = item;
                slots[i].quantity = amountToPlace;
                amount -= amountToPlace;

                if (amount <= 0)
                    return true;
            }
        }

        return false;
    }

    public void ClearSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return;
        slots[index].Clear();
    }

    public ChestSaveData GetSaveData()
    {
        ChestSaveData data = new ChestSaveData();

        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].IsEmpty())
            {
                ChestItemSaveData itemData = new ChestItemSaveData
                {
                    itemName = slots[i].item.itemName,
                    quantity = slots[i].quantity
                };

                data.items.Add(itemData);
            }
        }

        return data;
    }

public void LoadFromSaveData(ChestSaveData data, ItemDatabase itemDatabase)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Clear();
        }

        if (data == null || itemDatabase == null) return;

        foreach (ChestItemSaveData itemData in data.items)
        {
            ItemData item = itemDatabase.GetItemByName(itemData.itemName);
            if (item != null)
            {
                AddItem(item, itemData.quantity);
            }
        }
    }

    
}