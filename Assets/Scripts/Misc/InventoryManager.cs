using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour{
    
    public static InventoryManager Instance;

    public const int HotbarSize = 7;
    public const int InventorySize = 21;
    public const int TotalSlots = HotbarSize + InventorySize;

    public List<InventorySlot> slots = new List<InventorySlot>();

    public int selectedHotbarIndex = 0;

    public event Action OnInventoryChanged;
    public event Action<int> OnSelectedHotbarChanged;


    private void Awake(){
        
        if (Instance != null && Instance != this){

            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeInventory();
    }

    private void InitializeInventory(){

        if (slots.Count == 0){

            for (int i = 0; i < TotalSlots; i++){

                slots.Add(new InventorySlot());
            }
        }
    }

    public bool AddItem(ItemData item, int amount = 1){

        if (item == null || amount <= 0) return false;

        if (item.stackable){

            for (int i = 0; i < slots.Count; i++){

                if (slots[i].item == item && slots[i].quantity < item.maxStack){

                    int spaceLeft = item.maxStack - slots[i].quantity;
                    int amountToAdd = Mathf.Min(spaceLeft, amount);

                    slots[i].quantity += amountToAdd;
                    amount -= amountToAdd;

                    if (amount <= 0){

                        NotifyInventoryChanged();
                        return true;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Count; i++){


            if (slots[i].IsEmpty()){

                int amountToPlace = item.stackable ? Mathf.Min(item.maxStack, amount) : 1;

                slots[i].item = item;
                slots[i].quantity = amountToPlace;
                amount -= amountToPlace;

                if (amount <= 0){

                    NotifyInventoryChanged();
                    return true;
                }
            }
        }

        NotifyInventoryChanged();
        return false;
    }

    public bool RemoveItem(ItemData item, int amount = 1){

        if (item == null || amount <= 0) return false;

        for (int i = 0; i < slots.Count; i++){

            if (slots[i].item == item){

                int amountToRemove = Mathf.Min(slots[i].quantity, amount);
                slots[i].quantity -= amountToRemove;
                amount -= amountToRemove;

                if (slots[i].quantity <= 0){
                    slots[i].Clear();
                }

                if (amount <= 0){

                    NotifyInventoryChanged();
                    return true;
                }
            }
        }

        NotifyInventoryChanged();
        return false;
    }

    public InventorySlot GetHotbarSlot(int hotbarIndex){

        if (hotbarIndex < 0 || hotbarIndex >= HotbarSize) return null;
        return slots[hotbarIndex];

    }

    public InventorySlot GetSelectedHotbarSlot(){
        return GetHotbarSlot(selectedHotbarIndex);
    }



    public void SelectHotbarSlot(int index){

        if (index < 0 || index >= HotbarSize) return;

        selectedHotbarIndex = index;
        OnSelectedHotbarChanged?.Invoke(selectedHotbarIndex);
    }


    public void CycleHotbar(int direction){

        selectedHotbarIndex += direction;

        if (selectedHotbarIndex < 0)
            selectedHotbarIndex = HotbarSize - 1;

        else if (selectedHotbarIndex >= HotbarSize)
            selectedHotbarIndex = 0;

        OnSelectedHotbarChanged?.Invoke(selectedHotbarIndex);
    }

    public void UseSelectedItem(){

        InventorySlot slot = GetSelectedHotbarSlot();
        if (slot == null || slot.IsEmpty()) return;

        if (slot.item.energyRestoreAmount > 0){

            if (PlayerEnergy.Instance != null){

                PlayerEnergy.Instance.RestoreEnergy(slot.item.energyRestoreAmount);
            }


            slot.quantity--;

            if (slot.quantity <= 0){

                slot.Clear();
            }

            NotifyInventoryChanged();
        }
    }


    public void SwapSlots(int indexA, int indexB){

        if (indexA < 0 || indexA >= slots.Count) return;
        if (indexB < 0 || indexB >= slots.Count) return;
        if (indexA == indexB) return;


        InventorySlot temp = new InventorySlot{

            item = slots[indexA].item,
            quantity = slots[indexA].quantity
        };

        slots[indexA].item = slots[indexB].item;
        slots[indexA].quantity = slots[indexB].quantity;

        slots[indexB].item = temp.item;
        slots[indexB].quantity = temp.quantity;

        NotifyInventoryChanged();
    }

    public bool ConsumeSelectedItemForEnergy(){

        InventorySlot slot = GetSelectedHotbarSlot();

        if (slot == null || slot.IsEmpty() || slot.item == null)
            return false;

        ItemData item = slot.item;

        if (item.energyRestoreAmount <= 0)
            return false;

        if (PlayerEnergy.Instance != null){

            PlayerEnergy.Instance.RestoreEnergy(item.energyRestoreAmount);
        }

        slot.quantity--;

        if (slot.quantity <= 0){
            slot.Clear();
        }

        ForceRefresh();
        return true;
    }


    



    public void ForceRefresh(){

        OnInventoryChanged?.Invoke();
        OnSelectedHotbarChanged?.Invoke(selectedHotbarIndex);
    }


    private void NotifyInventoryChanged(){
        OnInventoryChanged?.Invoke();
    }
}