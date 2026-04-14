using UnityEngine;

[CreateAssetMenu(fileName = "NewCrop", menuName = "Farming/Crop Data")]
public class CropData : ScriptableObject{
    [Header("Basic Info")]
    public string cropID;
    public string cropName;

    [Header("Items")]
    public ItemData seedItem;
    public ItemData harvestItem;

    [Header("Growth Sprites")]
    public Sprite stage0SeedSprite;
    public Sprite stage1MidSprite;
    public Sprite stage2FullSprite;

    [Header("Growth Timing")]
    public int totalGrowthDays = 3;

    [Header("Harvest")]
    public int harvestAmount = 1;

    [Header("XP Rewards")]
    public int harvestXP = 10;
}