using UnityEngine;

public enum ItemType{
    Seed,
    Crop,
    Tool,
    Food,
    Material,
    Weapon
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject{
    
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;
    public ItemType itemType;

    [Header("Stacking")]
    public bool stackable = true;
    public int maxStack = 99;

    [Header("Food / Energy")]
    public int energyRestoreAmount = 0;

    [Header("Weapon Data")]
    public float weaponDamage = 0f;

    [Header("Seed Data")]
    public bool isPlantable = false;
    public CropData cropToPlant;
}