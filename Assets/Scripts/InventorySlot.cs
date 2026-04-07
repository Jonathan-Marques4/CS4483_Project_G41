using UnityEngine;

[System.Serializable]

public class InventorySlot{


    public ItemData item;
    public int quantity;

    public bool IsEmpty(){
        return item == null || quantity <= 0;
    }

    public void Clear(){

        item = null;
        quantity = 0;
    }
}