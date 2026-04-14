using UnityEngine;

public class ChestStateHandler : MonoBehaviour
{
    private PersistentObjectID objectID;
    private ChestInventory chestInventory;

    private void Awake()
    {
        objectID = GetComponent<PersistentObjectID>();
        chestInventory = GetComponent<ChestInventory>();
    }

    private void Start()
    {
        LoadState();
    }

    public void SaveState()
    {
        if (objectID == null || chestInventory == null || SceneStateManager.Instance == null) return;

        SceneStateManager.Instance.SaveChestState(objectID.objectID, chestInventory.GetSaveData());
    }

    public void LoadState()
    {
        if (objectID == null || chestInventory == null || SceneStateManager.Instance == null || ItemDatabase.Instance == null) return;

        if (SceneStateManager.Instance.TryGetChestState(objectID.objectID, out ChestSaveData data))
        {
            chestInventory.LoadFromSaveData(data, ItemDatabase.Instance);
        }
    }
}